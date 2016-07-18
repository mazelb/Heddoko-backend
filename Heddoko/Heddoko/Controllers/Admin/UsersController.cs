﻿using DAL;
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
        const string IsDeleted = "IsDeleted";
        const string License = "License";

        public override KendoResponse<IEnumerable<UserAPIModel>> Get([FromUri]KendoRequest request)
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
                    throw new Exception(i18n.Resources.WrongObjectAccess);
                }
            }


            if (request != null)
            {
                if (request.Filter != null)
                {
                    KendoFilterItem adminFilter = request.Filter.Get(Admin);
                    if (adminFilter != null)
                    {
                        if (CurrentUser.Role == UserRoleType.Admin)
                        {
                            items = UoW.UserRepository.Admins();
                        }
                    }

                    if (items == null)
                    {
                        KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                        if (isDeletedFilter != null)
                        {
                            isDeleted = true;
                        }

                        KendoFilterItem licenseFilter = request.Filter.Get(License);
                        if (licenseFilter != null
                        && !string.IsNullOrEmpty(licenseFilter.Value))
                        {
                            licenseID = int.Parse(licenseFilter.Value);
                        }

                        KendoFilterItem searchFilter = request.Filter.Get(Search);
                        if (searchFilter != null
                        && !string.IsNullOrEmpty(searchFilter.Value))
                        {
                            items = UoW.UserRepository.Search(searchFilter.Value, forceOrganization ? CurrentUser.OrganizationID : null, isDeleted, licenseID);
                        }

                        if (items == null
                        && licenseID.HasValue
                        && CurrentUser.OrganizationID.HasValue)
                        {
                            items = UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted, licenseID);
                        }
                    }
                }
            }

            if (items == null)
            {
                if (CurrentUser.Role == UserRoleType.Admin)
                {
                    items = UoW.UserRepository.All(isDeleted);
                }
                else
                {
                    items = UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted, licenseID);
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
                    UoW.UserRepository.ClearCache(item);
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

                        UoW.UserRepository.ClearCache(item);

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

                if (item.Role == UserRoleType.Worker
                 || item.Role == UserRoleType.Analyst)
                {
                    item.Role = UserRoleType.User;
                }

                UoW.Save();

                UoW.UserRepository.ClearCache(item);
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
                        item.FirstName = model.Firstname?.Trim();
                        item.LastName = model.Lastname?.Trim();
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
                if (CurrentUser.Role != UserRoleType.Admin)
                {
                    if (item.OrganizationID == null
                    || CurrentUser.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(i18n.Resources.WrongObjectAccess);
                    }
                }
            }

            if (model.Status.HasValue)
            {
                if (item.Status != UserStatusType.Invited)
                {
                    if (model.Status.Value != UserStatusType.Invited)
                    {
                        item.Status = model.Status.Value;
                    }
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
                OrganizationName = item.Organization?.Name,
                LicenseStatus = item.License?.Status,
                ExpirationAt = item.License?.ExpirationAt
            };
        }
    }
}
