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
    [RoutePrefix("admin/api/licenses")]
    [AuthAPI(Roles = DAL.Constants.Roles.LicenseAdminAndAdmin)]
    public class LicensesController : BaseAdminController<License, LicenseAPIModel>
    {
        const string Search = "Search";
        const string OrganizationID = "OrganizationID";
        const string Used = "Used";


        public override KendoResponse<IEnumerable<LicenseAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<License> items = null;
            int count = 0;

            bool isForceOrganization = false;
            if (CurrentUser.Role == UserRoleType.LicenseAdmin)
            {
                isForceOrganization = true;

                if (!CurrentUser.OrganizationID.HasValue)
                {
                    throw new Exception(i18n.Resources.WrongObjectAccess);
                }
            }

            bool isUsed = false;

            if (request != null)
            {
                if (request.Filter != null)
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
                                        items = UoW.LicenseRepository.GetAvailableByOrganization(CurrentUser.OrganizationID.Value);
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
            }

            if (items == null 
            && !isUsed)
            {
                items = new List<License>();

                if (isForceOrganization)
                {
                    items = UoW.LicenseRepository.GetByOrganization(CurrentUser.OrganizationID.Value);
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

            List<LicenseAPIModel> itemsDefault = new List<LicenseAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new LicenseAPIModel()
                {
                    ID = 0,
                    Name = i18n.Resources.EmptyLicense
                });
            }

            itemsDefault.AddRange(items.ToList().Select(c => Convert(c)));

            return new KendoResponse<IEnumerable<LicenseAPIModel>>()
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<LicenseAPIModel> Get(int id)
        {
            License item = UoW.LicenseRepository.GetFull(id);

            return new KendoResponse<LicenseAPIModel>()
            {
                Response = Convert(item)
            };
        }

        [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
        public override KendoResponse<LicenseAPIModel> Post(LicenseAPIModel model)
        {
            LicenseAPIModel response = new LicenseAPIModel();

            if (ModelState.IsValid)
            {
                License item = new License();
                Bind(item, model);
                UoW.LicenseRepository.Create(item);

                // Task.Run(() => Mailer.SendInviteAdminEmail(item));

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<LicenseAPIModel>()
            {
                Response = response
            };
        }

        [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
        public override KendoResponse<LicenseAPIModel> Put(LicenseAPIModel model)
        {
            LicenseAPIModel response = new LicenseAPIModel();

            if (model.ID.HasValue)
            {
                License item = UoW.LicenseRepository.GetFull(model.ID.Value);
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
                        throw new ModelStateException()
                        {
                            ModelState = ModelState
                        };
                    }
                }
            }

            return new KendoResponse<LicenseAPIModel>()
            {
                Response = response
            };
        }

        [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
        public override KendoResponse<LicenseAPIModel> Delete(int id)
        {
            License item = UoW.LicenseRepository.Get(id);
            item.Status = LicenseStatusType.Deleted;
            UoW.Save();

            return new KendoResponse<LicenseAPIModel>()
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

            if (model.OrganizationID.HasValue)
            {
                if (!item.OrganizationID.HasValue
                 || (model.OrganizationID.Value != item.OrganizationID.Value && (item.Users != null && item.Users.Count() == 0)))
                {
                    Organization organization = UoW.OrganizationRepository.Get(model.OrganizationID.Value);
                    item.Organization = organization;
                }
                else
                {
                    if (model.OrganizationID.Value != item.OrganizationID.Value)
                    {
                        throw new Exception(i18n.Resources.LicenseUsed);
                    }
                }
            }

            if (item.Users != null
             && item.Users.Count() > model.Amount)
            {
                throw new Exception(i18n.Resources.LiceseAmountUsed);
            }

            item.Amount = (int)model.Amount;
            item.Status = model.Status;
            item.ExpirationAt = model.ExpirationAt.EndOfDay();
            item.Type = model.Type;

            return item;
        }

        protected override LicenseAPIModel Convert(License item)
        {
            if (item == null)
            {
                return null;
            }

            return new LicenseAPIModel()
            {
                Name = item.Name,
                ViewID = item.ViewID,
                ID = item.ID,
                Amount = (uint)item.Amount,
                Status = item.Status,
                OrganizationID = item.OrganizationID,
                ExpirationAt = item.ExpirationAt,
                Type = item.Type,
                Used = item.Users == null ? 0 : item.Users.Count()
            };
        }
    }
}
