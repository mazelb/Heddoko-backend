using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;
using Newtonsoft.Json;
using Services;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/assets")]
    public class AssetsAPIController : BaseAPIController
    {
        /// <summary>
        ///     Upload files
        /// </summary>
        /// <param name="kitID">The id of kit. optional</param>
        /// <param name="type">The type of upload. required</param>
        [Route("upload")]
        [HttpPost]
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
        public async Task<Asset> Upload()
        {
            AssetAPIViewModel model = new AssetAPIViewModel();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //TODO Moved to CustomMediaTypeFormater
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (string key in provider.FormData.AllKeys)
            {
                string[] enumerable = provider.FormData.GetValues(key);

                if (enumerable == null)
                {
                    continue;
                }

                foreach (string val in enumerable)
                {
                    switch (key.ToLower())
                    {
                        case "kitid":
                            if (!string.IsNullOrEmpty(val))
                            {
                                int kitID = 0;
                                if (!int.TryParse(val, out kitID))
                                {
                                    throw new APIException(ErrorAPIType.KitID, $"{Resources.Wrong} kitID");
                                }
                                model.KitID = kitID;
                            }
                            break;
                        case "serial":
                        case "label":
                            model.Label = val;
                            break;
                        case "type":
                            AssetType typeTmp;
                            if (!Enum.TryParse(val, true, out typeTmp))
                            {
                                int type = -1;
                                if (!int.TryParse(val, out type))
                                {
                                    throw new APIException(ErrorAPIType.AssetType, $"{Resources.Wrong} type");
                                }

                                if (!Enum.IsDefined(typeof(AssetType), type))
                                {
                                    throw new APIException(ErrorAPIType.AssetType, $"{Resources.Wrong} type");
                                }

                                typeTmp = (AssetType)type;
                            }

                            model.Type = typeTmp;

                            if (model.Type != AssetType.Log &&
                                model.Type != AssetType.Record &&
                                model.Type != AssetType.Setting &&
                                model.Type != AssetType.SystemLog)
                            {
                                throw new APIException(ErrorAPIType.AssetType, $"{Resources.Wrong} type");
                            }
                            break;
                    }
                }
            }

            Kit kit = null;
            if (model.KitID.HasValue)
            {
                kit = UoW.KitRepository.Get(model.KitID.Value);
            }

            if (kit == null
            && !string.IsNullOrEmpty(model.Label))
            {
                kit = UoW.KitRepository.Get(model.Label);
            }

            if (kit == null)
            {
                throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} Kit by KitID or Serial");
            }

            if (kit.UserID.HasValue)
            {
                if (CurrentUser.RoleType == UserRoleType.Worker)
                {
                    if (kit.UserID.Value != CurrentUser.ID)
                    {
                        throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.WrongObjectAccess} kitID");
                    }

                    if (kit.User.TeamID != CurrentUser.TeamID)
                    {
                        throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.WrongObjectAccess} Team");
                    }
                }
                else
                {
                    if (!kit.OrganizationID.HasValue)
                    {
                        throw new APIException(ErrorAPIType.KitID, $"{Resources.NonAssigned} OrganizationID");
                    }

                    if (kit.OrganizationID != CurrentUser.OrganizationID)
                    {
                        throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.WrongObjectAccess} OrganizationID");
                    }

                    if (kit.User.TeamID != CurrentUser.TeamID)
                    {
                        throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.WrongObjectAccess} Team");
                    }
                }
            }
            else
            {
                throw new APIException(ErrorAPIType.KitID, $"{Resources.NonAssigned} kitID");
            }

            Asset asset = new Asset
            {
                Type = model.Type,
                Proccessing = AssetProccessingType.New,
                Status = UploadStatusType.New,
                Kit = kit,
                User = kit.User
            };

            foreach (MultipartFileData file in provider.FileData)
            {
                asset.Name = JsonConvert.DeserializeObject(file.Headers.ContentDisposition.FileName).ToString();
                string path = AssetManager.Path($"{CurrentUser.ID}/{DateTime.Now.Ticks.ToString("x")}_{asset.Name}", asset.Type);

                Azure.Upload(path, DAL.Config.AssetsContainer, file.LocalFileName);
                File.Delete(file.LocalFileName);

                asset.Image = $"/{path}";
                break;
            }
            asset.Status = UploadStatusType.Uploaded;
            UoW.AssetRepository.Create(asset);

            return asset;
        }

        /// <summary>
        ///     List of files by organization
        /// </summary>
        /// <param name="userID">The filter by userID</param>
        /// <param name="take">The amount of take entries</param>
        /// <param name="skip">The amoun of skip entries</param>
        [Route("list/{take:int}/{skip:int?}")]
        [Route("list/{userID:int?}/{take:int}/{skip:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
        public ListAPIViewModel<Asset> List(int take = 100, int? userID = null, int? skip = 0)
        {
            if (!CurrentUser.OrganizationID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} organization");
            }


            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} team");
            }

            if (CurrentUser.Role == UserRoleType.Worker)
            {
                userID = CurrentUser.ID;
            }

            return new ListAPIViewModel<Asset>()
            {
                Collection = UoW.AssetRepository.GetRecordByOrganization(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value, take, skip, userID).ToList(),
                TotalCount = UoW.AssetRepository.GetRecordByOrganizationCount(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value, userID)
            };
        }
    }
}