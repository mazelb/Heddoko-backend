using DAL;
using DAL.Models;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/users")]
    [AuthAPI(Roles = DAL.Constants.Roles.LicenseAdminAndAdmin)]
    public class UsersController : BaseAdminController<User, UserAPIModel>
    {
        const string Search = "Search";
        const string Admin = "Admin";
        const int NoLicense = 0;

        public override KendoResponse<IEnumerable<UserAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<User> items = null;
            int count = 0;

            bool forceOrganization = false;
            if (CurrentUser.Role == UserRoleType.LicenseAdmin)
            {
                forceOrganization = true;

                if (!CurrentUser.OrganizationID.HasValue)
                {
                    throw new Exception(i18n.Resources.WrongObjectAccess);
                }
            }


            if (request != null)
            {
                if (request.Filter != null)
                {
                    switch (request.Filter.Filters.Count())
                    {

                        case 1:
                            KendoFilterItem searchFilter = request.Filter.Get(Search);
                            if (searchFilter != null
                            && !string.IsNullOrEmpty(searchFilter.Value))
                            {
                                items = UoW.UserRepository.Search(searchFilter.Value, forceOrganization ? CurrentUser.OrganizationID : null);
                                break;
                            }

                            KendoFilterItem adminFilter = request.Filter.Get(Search);
                            if (adminFilter != null)
                            {
                                if (CurrentUser.Role == UserRoleType.Admin)
                                {
                                    items = UoW.UserRepository.Admins();
                                }
                            }

                            break;
                    }
                }
            }

            if (items == null)
            {
                if (CurrentUser.Role == UserRoleType.Admin)
                {
                    items = UoW.UserRepository.All();
                }
                else
                {
                    items = UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value);
                }
            }

            count = items.Count();

            if (request != null)
            {
                if (request.Take.HasValue)
                {
                    items = items.Skip(request.Skip.Value)
                                 .Take(request.Take.Value);

                }
            }

            var result = items.ToList()
                              .Select(c => Convert(c));

            return new KendoResponse<IEnumerable<UserAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<UserAPIModel> Get(int id)
        {
            User item = UoW.UserRepository.Get(id);

            return new KendoResponse<UserAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<UserAPIModel> Post(UserAPIModel model)
        {
            UserAPIModel response = new UserAPIModel();

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
                    UoW.UserRepository.SetCache(item);
                }

                Task.Run(() => Mailer.SendInviteEmail(item));

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<UserAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<UserAPIModel> Put(UserAPIModel model)
        {
            UserAPIModel response = new UserAPIModel();

            if (model.ID.HasValue)
            {
                User item = UoW.UserRepository.GetFull(model.ID.Value);
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {

                        Bind(item, model);
                        UoW.Save();

                        UoW.UserRepository.SetCache(item);

                        response = Convert(item);
                    }
                    else
                    {
                        throw new ModelStateException()
                        {
                            ModelState = ModelState
                        };
                    }
                }
            }

            return new KendoResponse<UserAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<UserAPIModel> Delete(int id)
        {
            User item = UoW.UserRepository.Get(id);

            if (item.ID != CurrentUser.ID)
            {
                item.Status = UserStatusType.Deleted;
                item.License = null;

                UoW.Save();

                UoW.UserRepository.SetCache(item);
            }

            return new KendoResponse<UserAPIModel>()
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
            || item.ID == 0)
            {
                User user = null;
                if (model.ID.HasValue)
                {
                    user = UoW.UserRepository.Get(model.ID.Value);
                }
                else
                {
                    user = UoW.UserRepository.GetByEmail(model.Email?.ToLower().Trim());

                    if (user == null)
                    {
                        user = UoW.UserRepository.GetByUsername(model.Username?.ToLower().Trim());
                    }

                    if (user != null
                    && user.OrganizationID != null)
                    {
                        throw new Exception(i18n.Resources.UserAlreadyInOrganizations);
                    }

                    if (user == null)
                    {
                        item.Email = model.Email?.ToLower().Trim();
                        item.Username = model.Username?.ToLower().Trim();
                        item.Status = UserStatusType.Invited;
                        item.Role = UserRoleType.User;
                        item.Phone = model.Phone;
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
                if (item.OrganizationID == null
                || CurrentUser.OrganizationID.Value != item.OrganizationID.Value)
                {
                    throw new Exception(i18n.Resources.WrongObjectAccess);
                }
            }

            if (model.LicenseID.HasValue)
            {
                if (model.LicenseID.Value == NoLicense)
                {
                    item.License = null;
                    if (item.Role != UserRoleType.LicenseAdmin
                      && item.Role != UserRoleType.Admin)
                    {
                        item.Role = UserRoleType.User;
                    }
                }
                else
                {
                    License license = UoW.LicenseRepository.GetFull(model.LicenseID.Value);

                    if (license.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(i18n.Resources.WrongObjectAccess);
                    }

                    if (license.Users == null
                     || license.Users.Count() < license.Amount)
                    {
                        item.License = license;
                        if (item.Role != UserRoleType.LicenseAdmin
                         && item.Role != UserRoleType.Admin)
                        {
                            switch (item.License.Type)
                            {
                                case LicenseType.DataAnalysis:
                                    item.Role = UserRoleType.Analyst;
                                    break;
                                case LicenseType.DataCollection:
                                    item.Role = UserRoleType.Worker;
                                    break;
                            }
                        }
                    }
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

            return new UserAPIModel()
            {
                ID = item.ID,
                Name = item.Name,
                Email = item.Email,
                Firstname = item.FirstName,
                Lastname = item.LastName,
                Username = item.Username,
                Role = item.Role,
                Status = item.Status,
                LicenseID = item.LicenseID,
                Phone = item.Phone,
                LicenseName = item.License?.Name,
                OrganizationName = item.Organization?.Name
            };
        }
    }
}
