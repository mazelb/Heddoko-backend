/**
 * @file OrganizationsController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Hangfire;
using Heddoko.Helpers.DomainRouting.Http;
using Heddoko.Models;
using Heddoko.Models.Admin;
using i18n;
using Microsoft.AspNet.Identity;
using Constants = DAL.Constants;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/organizations")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class OrganizationsController : BaseAdminController<Organization, OrganizationAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        public OrganizationsController() { }

        public OrganizationsController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        public OrganizationsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, UnitOfWork uow)
            : base(userManager, signInManager, uow)
        {
        }

        public override KendoResponse<IEnumerable<OrganizationAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Organization> items = null;
            int count = 0;
            if (request != null)
            {
                if (request.Filter != null)
                {
                    switch (request.Filter.Filters.Count())
                    {
                        case 0:
                            items = UoW.OrganizationRepository.All();
                            break;
                        case 1:
                            foreach (KendoFilterItem filter in request.Filter.Filters)
                            {
                                switch (filter.Field)
                                {
                                    case Used:
                                        items = UoW.OrganizationRepository.All();
                                        break;
                                    case Search:
                                        if (!string.IsNullOrEmpty(filter.Value))
                                        {
                                            items = UoW.OrganizationRepository.Search(filter.Value);
                                        }

                                        if (items == null)
                                        {
                                            items = UoW.OrganizationRepository.All();
                                        }

                                        break;
                                    case IsDeleted:
                                        items = UoW.OrganizationRepository.All(true);
                                        break;
                                }
                            }
                            break;
                        case 2:
                            bool isDeleted = false;

                            KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);

                            if (isDeletedFilter != null)
                            {
                                isDeleted = true;
                            }

                            KendoFilterItem searchFilter = request.Filter.Get(Search);
                            if (!string.IsNullOrEmpty(searchFilter?.Value))
                            {
                                items = UoW.OrganizationRepository.Search(searchFilter.Value, isDeleted);
                            }

                            if (items == null)
                            {
                                items = UoW.OrganizationRepository.All(isDeleted);
                            }
                            break;
                    }
                }
                else
                {
                    items = UoW.OrganizationRepository.All();
                }
            }

            if (items == null)
            {
                items = new List<Organization>();
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            IEnumerable<OrganizationAPIModel> result = items.ToList()
                                                            .Select(Convert);

            return new KendoResponse<IEnumerable<OrganizationAPIModel>>
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<OrganizationAPIModel> Get(int id)
        {
            Organization item = UoW.OrganizationRepository.Get(id);

            return new KendoResponse<OrganizationAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<OrganizationAPIModel> Post(OrganizationAPIModel model)
        {
            OrganizationAPIModel response;

            if (ModelState.IsValid)
            {
                Organization item = new Organization();
                Bind(item, model);
                UoW.OrganizationRepository.Create(item);

                int organizationID = item.Id;
                string inviteToken = UserManager.GenerateInviteToken(item.User.Id);
                UserManager.SendInviteAdminEmail(organizationID, inviteToken);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<OrganizationAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<OrganizationAPIModel> Put(OrganizationAPIModel model)
        {
            OrganizationAPIModel response = new OrganizationAPIModel();

            if (model.ID.HasValue)
            {
                Organization item = UoW.OrganizationRepository.GetFull(model.ID.Value);
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {
                        Bind(item, model);
                        UoW.Save();

                        response = Convert(item);
                    }
                    else
                    {
                        throw new ModelStateException
                        {
                            ModelState = ModelState
                        };
                    }
                }
            }

            return new KendoResponse<OrganizationAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<OrganizationAPIModel> Delete(int id)
        {
            Organization item = UoW.OrganizationRepository.GetFull(id);
            item.Status = OrganizationStatusType.Deleted;

            List<User> users = UoW.UserRepository.GetByOrganization(item.Id).ToList();

            foreach (User user in users)
            {
                user.Status = UserStatusType.Deleted;

                if (UserManager.IsInRole(user.Id, Constants.Roles.Worker))
                {
                    UserManager.RemoveFromRole(user.Id, Constants.Roles.Worker);
                }

                if (UserManager.IsInRole(user.Id, Constants.Roles.Analyst))
                {
                    UserManager.RemoveFromRole(user.Id, Constants.Roles.Analyst);
                }

                UoW.UserRepository.ClearCache(user);
            }

            UoW.Save();

            return new KendoResponse<OrganizationAPIModel>
            {
                Response = Convert(item)
            };
        }

        protected override Organization Bind(Organization item, OrganizationAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (model.User != null
                &&
                item.User == null)
            {
                User user = null;
                if (model.User.ID.HasValue)
                {
                    user = UoW.UserRepository.Get(model.User.ID.Value);
                }
                else
                {
                    user = UoW.UserRepository.GetByEmail(model.User.Email?.Trim()) ?? UoW.UserRepository.GetByUsername(model.User.Username?.ToLower().Trim());

                    if (user?.OrganizationID != null)
                    {
                        throw new Exception(Resources.UserAlreadyInOrganizations);
                    }

                    if (user == null)
                    {
                        user = new User
                        {
                            FirstName = model.User.Firstname?.Trim(),
                            LastName = model.User.Lastname?.Trim(),
                            Email = model.User.Email?.Trim(),
                            UserName = model.User.Username?.ToLower().Trim(),
                            Status = UserStatusType.Invited,
                            //TODO REMOVE THAT AFTER Migration all accounts
                            Role = UserRoleType.LicenseAdmin,
                            PhoneNumber = model.Phone
                        };

                        IdentityResult result = UserManager.Create(user);
                        if (result.Succeeded)
                        {
                            UserManager.AddToRole(user.Id, Constants.Roles.User);
                            UserManager.AddToRole(user.Id, DAL.Constants.Roles.LicenseAdmin);
                        }
                        else
                        {
                            throw new Exception(Resources.Wrong);
                        }
                    }
                    else
                    {
                        if (UserManager.IsInRole(user.Id, Constants.Roles.Admin))
                        {
                            throw new Exception(Resources.WrongLicenseAdmin);
                        }

                        user.Status = UserStatusType.Active;
                    }
                }

                item.User = user;
                user.Organization = item;

                UoW.UserRepository.ClearCache(user);
            }

            item.Name = model.Name;
            item.Phone = model.Phone;
            item.Address = model.Address;
            item.Notes = model.Notes;
            item.Status = model.Status;

            if (item.Status == OrganizationStatusType.Active)
            {
                if (item.User.Status != UserStatusType.Invited)
                {
                    item.User.Status = UserStatusType.Active;
                }
            }


            return item;
        }

        protected override OrganizationAPIModel Convert(Organization item)
        {
            if (item == null)
            {
                return null;
            }

            return new OrganizationAPIModel
            {
                ID = item.Id,
                Name = item.Name,
                Phone = item.Phone,
                Address = item.Address,
                UserID = item.UserID,
                Notes = item.Notes,
                IDView = item.IDView,
                Status = item.Status,
                User = new UserAPIModel
                {
                    Email = item.User.Email,
                    Name = item.User.Name,
                    Username = item.User.UserName
                },
                DataCollectorAmount = item.Licenses?.Where(c => c.ExpirationAt >= DateTime.Now
                                                                && c.Status == LicenseStatusType.Active).Sum(c => c.Amount)
            };
        }

        [DomainRoute("change", Constants.ConfigKeyName.DashboardSite)]
        [HttpPost]
        public bool Change(OrganizationAdminAPIModel model)
        {
            if (ModelState.IsValid)
            {
                Organization organization = UoW.OrganizationRepository.GetFull(model.OrganizationID);
                User user = UoW.UserRepository.GetFull(model.UserID);

                if (user != null
                    &&
                    organization != null)
                {
                    if (organization.Id == user.OrganizationID)
                    {
                        if (UserManager.IsInRole(organization.UserID, Constants.Roles.LicenseAdmin))
                        {
                            UserManager.RemoveFromRole(organization.UserID, Constants.Roles.LicenseAdmin);
                        }

                        organization.User = user;

                        if (!UserManager.IsInRole(user.Id, Constants.Roles.LicenseAdmin))
                        {
                            UserManager.AddToRole(user.Id, Constants.Roles.LicenseAdmin);
                        }

                        UoW.Save();

                        UoW.UserRepository.ClearCache(user);
                        UoW.UserRepository.ClearCache(organization.User);
                    }
                }
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }
            return true;
        }

        [DomainRoute("approve", Constants.ConfigKeyName.DashboardSite)]
        [HttpPost]
        public async Task<bool> Approve(OrganizationAdminAPIModel model)
        {
            if (ModelState.IsValid)
            {
                Organization organization = UoW.OrganizationRepository.GetFull(model.OrganizationID);

                if (organization?.User.Status == UserStatusType.Pending)
                {
                    organization.User.Status = UserStatusType.Invited;

                    organization.Status = OrganizationStatusType.Active;

                    UoW.Save();

                    UoW.UserRepository.ClearCache(organization.User);

                    int userId = organization.User.Id;
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(userId);
                    UserManager.SendActivationEmail(userId, code);
                }
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }
            return true;
        }

        [Route("login")]
        [HttpPost]
        public async Task<SinginAPIResponse> SignInAsOrganization(OrganizationAdminAPIModel model)
        {
            if (ModelState.IsValid)
            {
                User user = UoW.UserRepository.Get(model.UserID);
                user.ParentLoggedInUserId = CurrentUser.Id;

                await SignInManager.SignInAsync(user, false, false);

                await UserManager.UpdateToIdentityRoles(user);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new SinginAPIResponse
            {
                RedirectUrl = Url.Link("Default", new { controller = "Default", action = "Index" })
            };
        }
    }
}