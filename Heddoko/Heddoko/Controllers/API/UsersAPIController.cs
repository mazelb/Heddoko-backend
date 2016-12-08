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
using Heddoko.Helpers.DomainRouting.Http;
using DAL.Models.Enum;
using DAL.Models.MongoDocuments.Notifications;
using Hangfire;
using Heddoko.Models;
using Heddoko.Models.API;
using i18n;
using Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Constants = DAL.Constants;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/users")]
    public class UsersAPIController : BaseAPIController
    {
        public UsersAPIController()
        {
        }

        public UsersAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow)
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [DomainRoute("{id:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public User Get(int? id = null)
        {
            if (!id.HasValue)
            {
                return CurrentUser;
            }

            User user = UoW.UserRepository.GetIDCached(id.Value);
            if (user == null)
            {
                throw new APIException(ErrorAPIType.UserNotFound, ErrorAPIType.UserNotFound.GetDisplayName());
            }

            if (CurrentUser.Id == id.Value)
            {
                return user;
            }

            throw new APIException(ErrorAPIType.WrongObjectAccess, ErrorAPIType.WrongObjectAccess.GetDisplayName());
        }

        /// <summary>
        ///     Get profile of current user
        /// </summary>
        [DomainRoute("profile", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public User Profile()
        {
            CurrentUser.AllowLicenseInfoToken();
            User user = UoW.UserRepository.GetFull(CurrentUser.Id);
            user.AllowLicenseInfoToken();
            return user;
        }

        /// <summary>
        ///     Sign in user
        /// </summary>
        /// <param name="username">The username of user.</param>
        /// <param name="password">The password of user.</param>
        [DomainRoute("signin", Constants.ConfigKeyName.DashboardSite)]
        [HttpPost]
        public User Signin(SignInAPIViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            User user = UoW.UserRepository.GetByUsernameFull(model.Username?.Trim());
            if (user == null)
            {
                throw new APIException(ErrorAPIType.EmailOrPassword, Resources.WrongUsernameOrPassword);
            }

            if (user.IsBanned)
            {
                throw new APIException(ErrorAPIType.UserIsBanned, Resources.UserIsBanned);
            }

            if (user.IsNotApproved)
            {
                throw new APIException(ErrorAPIType.UserIsNotApproved, Resources.UserIsNotApproved);
            }

            if (user.IsActive)
            {
                //if (!PasswordHasher.Equals(model.Password?.Trim(), user.Salt, user.Password))
                //{
                //    throw new APIException(ErrorAPIType.EmailOrPassword, Resources.WrongUsernameOrPassword);
                //}

                if (user.License == null)
                {
                    throw new APIException(ErrorAPIType.LicenseIsNotReady, Resources.LicenseIsNotReady);
                }

                if (user.License.Validate())
                {
                    UoW.Save();
                    UoW.UserRepository.ClearCache(user);

                    if (user.License.Status == LicenseStatusType.Expired && user.License.OrganizationID.HasValue)
                    {
                        BackgroundJob.Enqueue(() => ActivityService.NotifyLicenseExpiredToOrganization(user.License.OrganizationID.Value, user.License.Id));
                    }
                }

                try
                {
                    UserManager.CheckUserLicense(user);
                }
                catch (Exception e)
                {
                    throw new APIException(ErrorAPIType.LicenseIsNotReady, e.Message);
                }

                if (user.AllowToken())
                {
                    user.AllowLicenseInfoToken();
                    return user;
                }

                user.Tokens.Add(new AccessToken
                {
                    Token = user.GenerateToken()
                });
                UoW.Save();
                user.AllowToken();
                UoW.UserRepository.SetCache(user);

                user.AllowLicenseInfoToken();

                return user;
            }

            throw new APIException(ErrorAPIType.UserIsNotActive, Resources.UserIsNotActive);
        }

        /// <summary>
        ///     Check if token is valid
        /// </summary>
        [DomainRoute("check", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public object Check()
        {
            return new
            {
                result = true
            };
        }

        /// <summary>
        ///     List of users by organization
        /// </summary>
        /// <param name="take">The amount of take entries</param>
        /// <param name="skip">The amoun of skip entries</param>
        [DomainRoute("list/{take:int}/{skip:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.Analyst)]
        public ListAPIViewModel<User> List(int take = 100, int? skip = 0)
        {
            if (!CurrentUser.OrganizationID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} organization");
            }

            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NonAssigned} team");
            }

            return new ListAPIViewModel<User>()
            {
                Collection = UoW.UserRepository.GetByOrganizationAPI(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value, take, skip).ToList(),
                TotalCount = UoW.UserRepository.GetByOrganizationAPICount(CurrentUser.OrganizationID.Value, CurrentUser.TeamID.Value)
            };
        }

        [Route("activity/{take:int}/{skip:int}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ListAPIViewModel<UserEvent> Activity(int take = 100, int skip = 0)
        {
            List<UserEvent> latestActivity = UoW.UserActivityRepository.GetLatestUserActivity(CurrentUser.Id, take, skip).ToList();

            UoW.UserActivityRepository.UpdateMany(latestActivity.Where(e => e.ReadStatus == ReadStatus.Unread).Select(e => e.Id).ToList(), ReadStatus.Read, DateTime.Now);

            return new ListAPIViewModel<UserEvent>
            {
                Collection = latestActivity,
                TotalCount = (int)UoW.UserActivityRepository.Count(CurrentUser.Id)
            };
        }

        [Route("activity/unread/{take:int}/{skip:int}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ListAPIViewModel<UserEvent> UnreadActivity(int take = 100, int skip = 0)
        {
            List<UserEvent> latestActivity = UoW.UserActivityRepository.GetUnreadUserActivity(CurrentUser.Id, take, skip).ToList();

            UoW.UserActivityRepository.UpdateMany(latestActivity.Select(e => e.Id).ToList(), ReadStatus.Read, DateTime.Now);

            return new ListAPIViewModel<UserEvent>
            {
                Collection = latestActivity,
                TotalCount = (int)UoW.UserActivityRepository.UnreadCount(CurrentUser.Id)
            };
        }

        [Route("subscribe")]
        [HttpPost]
        [AuthAPI(Roles = Constants.Roles.All)]
        public bool Subscribe(SubscribeTokenAPIViewModel model)
        {
            if (ModelState.IsValid)
            {
                Device device = UoW.DeviceRepository.GetByToken(model.Token.NormalizeToken());
                if (device != null)
                {
                    UoW.DeviceRepository.Delete(device);
                }

                device = new Device
                {
                    Status = DeviceStatus.Active,
                    Type = model.Type,
                    Token = model.Token.NormalizeToken(),
                    UserID = CurrentUser.Id
                };

                UoW.DeviceRepository.Create(device);

                return true;
            }

            throw new ModelStateException
            {
                ModelState = ModelState
            };
        }

        [Route("unsubscribe")]
        [HttpPost]
        [AuthAPI(Roles = Constants.Roles.All)]
        public bool UnSubscribe(UnsubscribeTokenAPIViewModel model)
        {
            if (ModelState.IsValid)
            {
                Device device = UoW.DeviceRepository.GetByToken(model.Token.NormalizeToken());
                if (device != null)
                {
                    UoW.DeviceRepository.Delete(device);
                    return true;
                }

                return false;
            }

            throw new ModelStateException
            {
                ModelState = ModelState
            };
        }

        /// <summary>
        ///     List of records by organization
        /// </summary>
        /// <param name="userID">The filter by userID</param>
        /// <param name="take">The amount of take entries</param>
        /// <param name="skip">The amoun of skip entries</param>
        [Route("records/{take:int}/{skip:int?}")]
        [Route("{userID:int?}/records/{take:int}/{skip:int?}")]
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

        [Route("records/upload")]
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
                        case "files":
                            model.FileTypes = JsonConvert.DeserializeObject<List<AssetFileAPIViewModel>>(val);

                            foreach (AssetFileAPIViewModel file in model.FileTypes)
                            {
                                if (!file.Type.IsRecordType())
                                {
                                    throw new APIException(ErrorAPIType.AssetType, $"{Resources.Wrong} type");
                                }
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

            if (provider.FileData.Count < Constants.Records.MinFilesCount || provider.FileData.Count > Constants.Records.MaxFilesCount)
            {
                throw new APIException(ErrorAPIType.FileData, string.Format(Resources.WrongFilesCount, Constants.Records.MinFilesCount, Constants.Records.MaxFilesCount));
            }

            Record record = new Record
            {
                Kit = kit,
                User = kit.User,
                Type = RecordType.Record
            };

            UoW.RecordRepository.Create(record);

            for (int i = 0; i < provider.FileData.Count; i++)
            {
                MultipartFileData file = provider.FileData[i];

                string name;
                try
                {
                    name = JsonConvert.DeserializeObject(file.Headers.ContentDisposition.FileName).ToString();
                }
                catch (JsonReaderException)
                {
                    name = file.Headers.ContentDisposition.FileName;
                }

                if (model.FileTypes[i].FileName != name)
                {
                    throw new APIException(ErrorAPIType.FileData, $"{Resources.Wrong} file data");
                }

                Asset asset = new Asset
                {
                    Type = model.FileTypes[i].Type,
                    Proccessing = AssetProccessingType.New,
                    Status = UploadStatusType.New,
                    Kit = kit,
                    User = kit.User,
                    Record = record,
                    Name = name
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
    }
}