using System;
using System.IO;
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
        ///     Uplaod files
        /// </summary>
        /// <param name="kitID">The id of kit. optional</param>
        /// <param name="type">The type of upload. required</param>
        [Route("upload")]
        [HttpPost]
        [AuthAPI(Roles = Constants.Roles.All)]
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
                if (kit.UserID.HasValue)
                {
                    if (kit.UserID.Value != CurrentUser.ID)
                    {
                        //TODO block wrong users
                        //throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.WrongObjectAccess} kitID");
                    }
                }
                else
                {
                    //TODO block non assinged kits
                    //throw new APIException(ErrorAPIType.KitID, $"{Resources.NonAssigned} kitID");
                }
            }

            Asset asset = new Asset
            {
                Type = model.Type,
                Proccessing = AssetProccessingType.New,
                Status = UploadStatusType.New,
                Kit = kit,
                User = CurrentUser
            };

            foreach (MultipartFileData file in provider.FileData)
            {
                string path = AssetManager.Path($"{CurrentUser.ID}/{DateTime.Now.Ticks.ToString("x")}_{JsonConvert.DeserializeObject(file.Headers.ContentDisposition.FileName)}", asset.Type);

                Azure.Upload(path, DAL.Config.AssetsContainer, file.LocalFileName);
                File.Delete(file.LocalFileName);

                asset.Image = $"/{path}";
                break;
            }
            asset.Status = UploadStatusType.Uploaded;
            UoW.AssetRepository.Create(asset);

            return asset;
        }
    }
}