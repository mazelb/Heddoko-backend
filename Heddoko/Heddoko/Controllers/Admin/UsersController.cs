using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/users")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndAdmin)]
    public class UsersController : BaseAdminController<User, UserAPIModel>
    {
        private const string Search = "Search";
        private const string Admin = "Admin";
        private const int NoLicense = 0;
        private const int NoKit = 0;
        private const string IsDeleted = "IsDeleted";
        private const string License = "License";
        private const string Used = "Used";


        public override KendoResponse<IEnumerable<UserAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<User> items = null;
            int count = 0;

            bool forceOrganization = false;
            bool isDeleted = false;
            int? licenseID = null;

            if (CurrentUser.Role == UserRoleType.LicenseAdmin)
            {
                forceOrganization = true;

                if (!CurrentUser.OrganizationID.HasValue)
                {
                    throw new Exception(Resources.WrongObjectAccess);
                }
            }


            if (request?.Filter != null)
            {
                KendoFilterItem adminFilter = request.Filter.Get(Admin);
                if (adminFilter != null)
                {
                    if (CurrentUser.Role == UserRoleType.Admin)
                    {
                        items = UoW.UserRepository.Admins();
                    }
                }

                KendoFilterItem isUsedFilter = request.Filter.Get(Used);

                if (isUsedFilter != null)
                {
                    int tmp = 0;
                    int.TryParse(isUsedFilter.Value, out tmp);

                    items = UoW.UserRepository.GetByOrganization(tmp);
                }

                if (items == null)
                {
                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }

                    KendoFilterItem licenseFilter = request.Filter.Get(License);
                    if (!string.IsNullOrEmpty(licenseFilter?.Value))
                    {
                        licenseID = int.Parse(licenseFilter.Value);
                    }

                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    if (!string.IsNullOrEmpty(searchFilter?.Value))
                    {
                        items = UoW.UserRepository.Search(searchFilter.Value, forceOrganization ? CurrentUser.OrganizationID : null, isDeleted, licenseID);
                    }

                    if (items == null
                        &&
                        licenseID.HasValue
                        &&
                        CurrentUser.OrganizationID.HasValue)
                    {
                        items = UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted, licenseID);
                    }
                }
            }

            if (items == null)
            {
                items = CurrentUser.Role == UserRoleType.Admin ? UoW.UserRepository.All(isDeleted) : UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted, licenseID);
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            IEnumerable<UserAPIModel> result = items.ToList()
                                                    .Select(Convert);

            return new KendoResponse<IEnumerable<UserAPIModel>>
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<UserAPIModel> Get(int id)
        {
            User item = UoW.UserRepository.Get(id);

            return new KendoResponse<UserAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<UserAPIModel> Post(UserAPIModel model)
        {
            UserAPIModel response;

            if (ModelState.IsValid)
            {
                User item = new User();
                item = Bind(item, model);
                if (item.ID == 0)
                {
                    UoW.UserRepository.Create(item);
                }
                else
                {
                    UoW.Save();
                    UoW.UserRepository.ClearCache(item);
                }

                Task.Run(() => Mailer.SendInviteEmail(item));

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<UserAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<UserAPIModel> Put(UserAPIModel model)
        {
            UserAPIModel response = new UserAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<UserAPIModel>
                {
                    Response = response
                };
            }

            User item = UoW.UserRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<UserAPIModel>
                {
                    Response = response
                };
            }

            if (ModelState.IsValid)
            {
                Bind(item, model);
                UoW.Save();

                UoW.UserRepository.ClearCache(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<UserAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<UserAPIModel> Delete(int id)
        {
            User item = UoW.UserRepository.Get(id);

            if (item.ID == CurrentUser.ID)
            {
                return new KendoResponse<UserAPIModel>
                {
                    Response = Convert(item)
                };
            }

            item.Status = UserStatusType.Deleted;
            item.License = null;

            if (item.Role == UserRoleType.Worker
                ||
                item.Role == UserRoleType.Analyst)
            {
                item.Role = UserRoleType.User;
            }

            UoW.Save();

            UoW.UserRepository.ClearCache(item);

            return new KendoResponse<UserAPIModel>
            {
                Response = Convert(item)
            };
        }


        protected override User Bind(User item, UserAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (item == null
                ||
                item.ID == 0)
            {
                User user = null;
                if (model.ID.HasValue)
                {
                    user = UoW.UserRepository.Get(model.ID.Value);
                }
                else
                {
                    user = UoW.UserRepository.GetByEmail(model.Email?.ToLower().Trim()) ?? UoW.UserRepository.GetByUsername(model.Username?.ToLower().Trim());

                    if (user?.OrganizationID != null)
                    {
                        throw new Exception(Resources.UserAlreadyInOrganizations);
                    }

                    if (user == null)
                    {
                        item.Email = model.Email?.ToLower().Trim();
                        item.UserName = model.Username?.ToLower().Trim();
                        item.FirstName = model.Firstname?.Trim();
                        item.LastName = model.Lastname?.Trim();
                        item.Status = UserStatusType.Invited;
                        item.Role = UserRoleType.User;
                        item.PhoneNumber = model.Phone;
                        item.InviteToken = PasswordHasher.Md5(DateTime.Now.Ticks.ToString());
                        item.Organization = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);
                    }
                    else
                    {
                        user.Role = UserRoleType.User;
                        user.Status = UserStatusType.Active;
                        user.Organization = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);

                        item = user;
                    }
                }
            }
            else
            {
                if (CurrentUser.Role != UserRoleType.Admin)
                {
                    if (item.OrganizationID == null
                        ||
                        CurrentUser.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(Resources.WrongObjectAccess);
                    }
                }
            }

            if (model.Status.HasValue
             && item.Status != model.Status)
            {
                if (item.Status == UserStatusType.Invited)
                {
                    throw new Exception(Resources.CantChangeInvite);
                }

                if (model.Status == UserStatusType.Invited)
                {
                    throw new Exception(Resources.CantSetInvite);
                }

                if (item.Status == UserStatusType.Pending)
                {
                    throw new Exception(Resources.CantChangePending);
                }

                if (model.Status == UserStatusType.Pending)
                {
                    throw new Exception(Resources.CantSetPending);
                }

                item.Status = model.Status.Value;
            }

            if (model.TeamID.HasValue)
            {
                item.Team = UoW.TeamRepository.Get(model.TeamID.Value);
            }

            if (model.LicenseID.HasValue)
            {
                if (model.LicenseID.Value == NoLicense)
                {
                    item.License = null;
                    if (item.Role != UserRoleType.LicenseAdmin
                        &&
                        item.Role != UserRoleType.Admin)
                    {
                        item.Role = UserRoleType.User;
                    }
                }
                else
                {
                    License license = UoW.LicenseRepository.GetFull(model.LicenseID.Value);

                    if (license.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(Resources.WrongObjectAccess);
                    }

                    if (license.Users == null
                        ||
                        license.Users.Count() < license.Amount)
                    {
                        item.License = license;
                        if (item.Role == UserRoleType.LicenseAdmin ||
                            item.Role == UserRoleType.Admin)
                        {
                            return item;
                        }

                        switch (item.License.Type)
                        {
                            case LicenseType.DataAnalysis:
                                item.Role = UserRoleType.Analyst;
                                break;
                            case LicenseType.DataCollection:
                                item.Role = UserRoleType.Worker;
                                break;
                            case LicenseType.No:
                            default:
                                throw new Exception(Resources.WrongObjectAccess);
                        }
                    }
                }
            }

            if (model.KitID.HasValue)
            {
                if (model.KitID.Value == NoKit)
                {
                    UoW.KitRepository.RemoveUser(item.ID);
                }
                else
                {
                    Kit kit = UoW.KitRepository.GetFull(model.KitID.Value);

                    if (kit.OrganizationID == null
                     || kit.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(Resources.WrongObjectAccess);
                    }

                    if (kit.User != null)
                    {
                        throw new Exception($"{Resources.Kit} {Resources.AlreadyUsed}");
                    }

                    kit.User = item;
                }
            }

            return item;
        }

        protected override UserAPIModel Convert(User item)
        {
            if (item == null)
            {
                return null;
            }

            return new UserAPIModel
            {
                ID = item.ID,
                Name = item.Name,
                Email = item.Email,
                Firstname = item.FirstName,
                Lastname = item.LastName,
                Username = item.UserName,
                Role = item.Role,
                Status = item.Status,
                LicenseID = item.LicenseID ?? 0,
                Phone = item.PhoneNumber,
                LicenseName = item.License?.Name,
                OrganizationName = item.Organization?.Name,
                LicenseStatus = item.License?.Status,
                ExpirationAt = item.License?.ExpirationAt,
                KitID = item.Kit?.ID ?? 0,
                Kit = item.Kit,
                TeamID = item.TeamID ?? 0,
                Team = item.Team
            };
        }
    }
}