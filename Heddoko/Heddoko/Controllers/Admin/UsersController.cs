/**
 * @file UsersController.cs
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
using System.Web.Security;
using DAL;
using DAL.Models;
using Heddoko.Helpers.DomainRouting.Http;
using DAL.Models.Enum;
using Hangfire;
using Heddoko.Models;
using i18n;
using Microsoft.AspNet.Identity;
using Services;
using Constants = DAL.Constants;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/users")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndAnalystAndAdmin)]
    public class UsersController : BaseAdminController<User, UserAPIModel>
    {
        private const string Search = "Search";
        private const string Admin = "Admin";
        private const int NoLicense = 0;
        private const int NoKit = 0;
        private const string IsDeleted = "IsDeleted";
        private const string License = "License";
        private const string Used = "Used";
        private const string TeamID = "TeamID";
        private const int PasswordLength = 8;
        private const int PasswordNonNumerics = 1;

        public UsersController() { }

        public UsersController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        public override KendoResponse<IEnumerable<UserAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<User> items = null;
            int count = 0;

            bool forceOrganization = false;
            bool isDeleted = false;
            int? team = null;

            if (IsAnalyst && CurrentUser.TeamID.HasValue)
            {
                return GetTeamUsers(request);
            }

            if (!IsAdmin)
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
                    if (IsAdmin)
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

                    KendoFilterItem teamFilter = request.Filter.Get(TeamID);
                    if (teamFilter != null && CurrentUser.OrganizationID.HasValue)
                    {
                        int tmp;
                        int.TryParse(teamFilter.Value, out tmp);
                        team = tmp;
                    }

                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    if (!string.IsNullOrEmpty(searchFilter?.Value))
                    {
                        items = UoW.UserRepository.Search(searchFilter.Value, forceOrganization ? CurrentUser.OrganizationID : null, team, isDeleted);
                    }
                    else if (team.HasValue)
                    {
                        items = UoW.UserRepository.GetByTeam(team.Value, CurrentUser.OrganizationID.Value, isDeleted);
                    } 

                    if (items == null
                        &&
                        CurrentUser.OrganizationID.HasValue)
                    {
                        items = UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted);
                    }
                }
            }

            if (items == null)
            {
                items = IsAdmin ? UoW.UserRepository.All(isDeleted) : UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted);
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

        public KendoResponse<IEnumerable<UserAPIModel>> GetTeamUsers([FromUri] KendoRequest request)
        {
            IEnumerable<User> items = null;
            int count = 0;
            bool isDeleted = false;

            int? team = CurrentUser.TeamID;

            if (team.HasValue && CurrentUser.OrganizationID.HasValue)
            {
                if (request?.Filter != null)
                {
                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }

                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    if (!string.IsNullOrEmpty(searchFilter?.Value))
                    {
                        items = UoW.UserRepository.Search(searchFilter.Value, CurrentUser.OrganizationID, team, isDeleted);
                    }
                }
                if (items == null)
                {
                    items = UoW.UserRepository.GetByTeam(team.Value, CurrentUser.OrganizationID.Value, isDeleted);
                }
            }

            if (items == null)
            {
                items = new List<User>();
            }

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
                if (item.Id == 0)
                {
                    IdentityResult result = UserManager.Create(item);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(item.Id, item.Role.GetStringValue());
                    }
                }
                else
                {
                    UoW.Save();
                    UoW.UserRepository.ClearCache(item);
                }

                item.Role = model.Role;
                UserManager.UpdateUserIdentityRole(item);

                if (!item.Email.IsNullOrEmpty())
                {
                    int userID = item.Id;
                    //Invite Token
                    //string inviteToken = UserManager.GenerateInviteToken(item.Id);
                    //UserManager.SendInviteEmail(userID, inviteToken);

                    //Generated Password
                    string password = Membership.GeneratePassword(PasswordLength, PasswordNonNumerics);
                    UserManagerExtensions.AddPassword(UserManager, userID, password);
                    UserManager.SendPasswordEmail(userID, password);
                }
                item.Status = UserStatusType.Active;


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
                if (item.Role != model.Role)
                {
                    item.Role = model.Role;
                    UserManager.UpdateUserIdentityRole(item);
                }

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

            if (item.Id == CurrentUser.Id)
            {
                return new KendoResponse<UserAPIModel>
                {
                    Response = Convert(item)
                };
            }

            item.Status = UserStatusType.Deleted;

            UserManager.RemoveFromRoles(item.Id, Constants.Roles.Worker, Constants.Roles.Analyst);

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
                item.Id == 0)
            {
                User user = null;
                if (model.ID.HasValue)
                {
                    user = UoW.UserRepository.Get(model.ID.Value);
                }
                else
                {
                    user = UoW.UserRepository.GetByUsername(model.Username?.ToLower().Trim());

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
                        if(model.Role == UserRoleType.Admin)
                        {
                            item.Role = UserRoleType.User;
                        }
                        else
                        {
                            item.Role = model.Role;
                        }
                        item.PhoneNumber = model.Phone;
                        item.Organization = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);
                    }
                    else
                    {
                        if (model.Role == UserRoleType.Admin)
                        {
                            item.Role = UserRoleType.User;
                        }
                        else
                        {
                            item.Role = model.Role;
                        }
                        user.Status = UserStatusType.Active;
                        user.Organization = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);

                        item = user;
                    }
                }
            }
            else
            {
                if (!UserManager.IsInRole(CurrentUser.Id, Constants.Roles.Admin))
                {
                    if (item.OrganizationID == null
                        ||
                        CurrentUser.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(Resources.WrongObjectAccess);
                    }
                }

                User user = UoW.UserRepository.GetByEmail(model.Email?.ToLower().Trim());
                if (user != null && user.Id != item.Id)
                {
                    throw new Exception(Resources.EmailUsed);
                }

                item.Email = model.Email?.ToLower().Trim();
                item.FirstName = model.Firstname?.Trim();
                item.LastName = model.Lastname?.Trim();
                item.PhoneNumber = model.Phone;
            }

            if (model.Status.HasValue
             && item.Status != model.Status)
            {
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
                ID = item.Id,
                Name = item.Name,
                Email = item.Email,
                Firstname = item.FirstName,
                Lastname = item.LastName,
                Username = item.UserName,
                Role = item.Role,
                Status = item.Status,
                Phone = item.PhoneNumber,
                OrganizationName = item.Organization?.Name,
                TeamID = item.TeamID ?? 0,
                Team = item.Team
            };
        }

        [DomainRoute("activation/resend", Constants.ConfigKeyName.DashboardSite)]
        [HttpPost]
        public async Task<bool> ResendActivationEmail(UserAdminAPIModel model)
        {
            if (ModelState.IsValid)
            {
                User user = UoW.UserRepository.Get(model.UserId);

                if (user?.Status == UserStatusType.Invited)
                {
                    bool isNew = string.IsNullOrEmpty(user.PasswordHash);

                    if (isNew)
                    {
                        string code = await UserManager.GenerateInviteTokenAsync(user);
                        if (await UserManager.IsInRoleAsync(user.Id, Constants.Roles.LicenseAdmin))
                        {
                            UserManager.SendInviteAdminEmail(user.Organization.Id, code);
                        }
                        else
                        {
                            UserManager.SendInviteEmail(user.Id, code);
                        }
                    }
                    else
                    {
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        UserManager.SendActivationEmail(user.Id, code);
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
    }
}