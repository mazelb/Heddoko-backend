﻿/**
 * @file LicensesController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
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
    [RoutePrefix("admin/api/licenses")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndAdmin)]
    public class LicensesController : BaseAdminController<License, LicenseAPIModel>
    {
        private const string Search = "Search";
        private const string OrganizationID = "OrganizationID";
        private const string Used = "Used";

        public LicensesController() { }

        public LicensesController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        public override KendoResponse<IEnumerable<LicenseAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<License> items = null;
            int count = 0;

            bool isForceOrganization = false;
            if (UserManager.IsInRole(CurrentUser.Id, DAL.Constants.Roles.LicenseAdmin))
            {
                isForceOrganization = true;

                if (!CurrentUser.OrganizationID.HasValue)
                {
                    throw new Exception(Resources.WrongObjectAccess);
                }
            }

            bool isUsed = false;

            if (request?.Filter != null)
            {
                switch (request.Filter.Filters.Count())
                {
                    case 0:
                        items = UoW.LicenseRepository.GetByOrganization(CurrentUser.OrganizationID.Value);
                        break;
                    case 1:
                        foreach (KendoFilterItem filter in request.Filter.Filters)
                        {
                            switch (filter.Field)
                            {
                                case Used:
                                    int tmp = 0;
                                    int? usedID = null;
                                    if (int.TryParse(filter.Value, out tmp))
                                    {
                                        usedID = tmp;
                                    }

                                    items = UoW.LicenseRepository.GetAvailableByOrganization(CurrentUser.OrganizationID.Value, usedID);
                                    isUsed = true;
                                    break;
                                case Search:
                                    if (!string.IsNullOrEmpty(filter.Value))
                                    {
                                        items = UoW.LicenseRepository.Search(filter.Value, isForceOrganization ? CurrentUser.OrganizationID : null);
                                    }
                                    break;
                                case OrganizationID:
                                    if (!string.IsNullOrEmpty(filter.Value))
                                    {
                                        int organizationID = int.Parse(filter.Value);

                                        if (isForceOrganization)
                                        {
                                            organizationID = CurrentUser.OrganizationID.Value;
                                        }

                                        items = UoW.LicenseRepository.GetByOrganization(int.Parse(filter.Value));
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = new List<License>();

                if (isForceOrganization)
                {
                    items = UoW.LicenseRepository.GetByOrganization(CurrentUser.OrganizationID.Value);
                }
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<LicenseAPIModel> itemsDefault = new List<LicenseAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new LicenseAPIModel
                {
                    ID = 0,
                    Name = Resources.EmptyLicense
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<LicenseAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<LicenseAPIModel> Get(int id)
        {
            License item = UoW.LicenseRepository.GetFull(id);

            return new KendoResponse<LicenseAPIModel>
            {
                Response = Convert(item)
            };
        }

        [AuthAPI(Roles = Constants.Roles.Admin)]
        public override KendoResponse<LicenseAPIModel> Post(LicenseAPIModel model)
        {
            LicenseAPIModel response;

            if (ModelState.IsValid)
            {
                License item = new License();
                Bind(item, model);
                UoW.LicenseRepository.Create(item);

                // Task.Run(() => Mailer.SendInviteAdminEmail(item));
                BackgroundJob.Enqueue(() => LicenseManager.Check());
                BackgroundJob.Enqueue(() => ActivityService.NotifyLicenseAddedToOrganization(item.Organization.Id, item.Id));

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<LicenseAPIModel>
            {
                Response = response
            };
        }

        [AuthAPI(Roles = Constants.Roles.Admin)]
        public override KendoResponse<LicenseAPIModel> Put(LicenseAPIModel model)
        {
            LicenseAPIModel response = new LicenseAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<LicenseAPIModel>
                {
                    Response = response
                };
            }


            License item = UoW.LicenseRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<LicenseAPIModel>
                {
                    Response = response
                };
            }

            if (ModelState.IsValid)
            {
                Bind(item, model);
                UoW.Save();

                BackgroundJob.Enqueue(() => Services.LicenseManager.Check());

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<LicenseAPIModel>
            {
                Response = response
            };
        }

        [AuthAPI(Roles = Constants.Roles.Admin)]
        public override KendoResponse<LicenseAPIModel> Delete(int id)
        {
            License item = UoW.LicenseRepository.Get(id);
            item.Status = LicenseStatusType.Deleted;
            UoW.Save();

            if (item.OrganizationID != null)
            {
                BackgroundJob.Enqueue(() => ActivityService.NotifyLicenseRemovedFromOrganization(item.OrganizationID.Value, item.Id));
            }

            return new KendoResponse<LicenseAPIModel>
            {
                Response = Convert(item)
            };
        }

        protected override License Bind(License item, LicenseAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (!IsAdmin)
            {
                throw new Exception(Resources.WrongObjectAccess);
            }

            if (model.OrganizationID.HasValue)
            {
                if (!item.OrganizationID.HasValue
                    ||
                    (model.OrganizationID.Value != item.OrganizationID.Value && item.Users != null && !item.Users.Any()))
                {
                    Organization organization = UoW.OrganizationRepository.Get(model.OrganizationID.Value);
                    item.Organization = organization;
                }
                else
                {
                    if (model.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(Resources.LicenseUsed);
                    }
                }
            }

            item.Amount = (int)model.Amount;
            item.Status = model.Status;
            item.ExpirationAt = model.ExpirationAt.EndOfDay();
            item.Type = model.Type;
            item.Validate();

            return item;
        }

        protected override LicenseAPIModel Convert(License item)
        {
            if (item == null)
            {
                return null;
            }

            return new LicenseAPIModel
            {
                Name = item.Name,
                ViewID = item.ViewID,
                IDView = item.IDView,
                ID = item.Id,
                Amount = (uint)item.Amount,
                Status = item.Status,
                OrganizationID = item.OrganizationID,
                ExpirationAt = item.ExpirationAt,
                Type = item.Type,
                Used = item.Users?.Count() ?? 0
            };
        }
    }
}