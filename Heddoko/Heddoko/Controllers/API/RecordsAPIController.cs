using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DAL;
using DAL.Models;
using Heddoko.Models;
using Heddoko.Models.API;
using i18n;
using Newtonsoft.Json;
using Services;
using Microsoft.AspNet.Identity;
using Constants = DAL.Constants;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/userrecords")]
    public class RecordsAPIController : BaseAPIController
    {
        public RecordsAPIController()
        {
        }

        public RecordsAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow)
        {
        }

        [Route("upload")]
        [HttpPost]
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
        public async Task<Record> Upload()
        {
            RecordAPIViewModel model = new RecordAPIViewModel();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                UserManager.CheckUserLicense(CurrentUser);
            }
            catch (Exception e)
            {
                throw new APIException(ErrorAPIType.LicenseIsNotReady, e.Message);
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
                                int kitID;
                                if (!int.TryParse(val, out kitID))
                                {
                                    throw new APIException(ErrorAPIType.KitID, $"{Resources.Wrong} kitID");
                                }
                                model.KitID = kitID;
                            }
                            break;
                        case "label":
                            model.Label = val;
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
                if (UserManager.IsInRole(CurrentUser.Id, Constants.Roles.Worker))
                {
                    if (kit.UserID.Value != CurrentUser.Id)
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
                        throw new APIException(ErrorAPIType.WrongObjectAccess,
                            $"{Resources.WrongObjectAccess} OrganizationID");
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

            if (provider.FileData.Count < 1 || provider.FileData.Count > 3)
            {
                throw new APIException(ErrorAPIType.FileData, string.Format(Resources.WrongFilesCount, 1, 3));
            }

            Record record = new Record
            {
                Kit = kit,
                User = kit.User
            };

            UoW.RecordRepository.Create(record);

            foreach (MultipartFileData file in provider.FileData)
            {
                Asset asset = new Asset
                {
                    Type = AssetType.Record,
                    Proccessing = AssetProccessingType.New,
                    Status = UploadStatusType.New,
                    Kit = kit,
                    User = kit.User,
                    Record = record,
                    Name = JsonConvert.DeserializeObject(file.Headers.ContentDisposition.FileName).ToString()
                };

                string path = AssetManager.Path($"{CurrentUser.Id}/{DateTime.Now.Ticks:x}_{asset.Name}", asset.Type);

                Azure.Upload(path, DAL.Config.AssetsContainer, file.LocalFileName);
                File.Delete(file.LocalFileName);

                asset.Image = $"/{path}";

                asset.Status = UploadStatusType.Uploaded;
                UoW.AssetRepository.Create(asset);
            }

            return record;
        }

        /// <summary>
        ///     List of records by organization
        /// </summary>
        /// <param name="userID">The filter by userID</param>
        /// <param name="take">The amount of take entries</param>
        /// <param name="skip">The amoun of skip entries</param>
        [Route("list/{take:int}/{skip:int?}")]
        [Route("list/{userID:int?}/{take:int}/{skip:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
        public ListAPIViewModel<Record> List(int take = 100, int? userID = null, int? skip = 0)
        {
            if (!CurrentUser.OrganizationID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} organization");
            }

            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} team");
            }

            if (UserManager.IsInRole(CurrentUser.Id, Constants.Roles.Worker))
            {
                userID = CurrentUser.Id;
            }

            return new ListAPIViewModel<Record>
            {
                Collection = UoW.RecordRepository.GetRecordByOrganization(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value, take, skip, userID).ToList(),
                TotalCount = UoW.RecordRepository.GetRecordByOrganizationCount(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value, userID)
            };
        }
    }
}