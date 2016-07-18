using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/pants")]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public class PantsController : BaseAdminController<Pants, PantsAPIModel>
    {
        const string Search = "Search";
        const string IsDeleted = "IsDeleted";
        const string Used = "Used";
        const int NoPantsOctopiID = 0;

        public override KendoResponse<IEnumerable<PantsAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<Pants> items = null;
            int count = 0;

            bool isDeleted = false;

            if (request != null)
            {
                if (request.Filter != null)
                {
                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }

                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    if (searchFilter != null
                    && !string.IsNullOrEmpty(searchFilter.Value))
                    {
                        items = UoW.PantsRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null)
            {

                items = UoW.PantsRepository.All(isDeleted);
            }

            count = items.Count();

            if (request != null
             && request.Take.HasValue)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);

            }

            var result = items.ToList()
                              .Select(c => Convert(c));

            return new KendoResponse<IEnumerable<PantsAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<PantsAPIModel> Get(int id)
        {
            Pants item = UoW.PantsRepository.Get(id);

            return new KendoResponse<PantsAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<PantsAPIModel> Post(PantsAPIModel model)
        {
            PantsAPIModel response = new PantsAPIModel();

            if (ModelState.IsValid)
            {
                Pants item = new Pants();

                Bind(item, model);

                UoW.PantsRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<PantsAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<PantsAPIModel> Put(PantsAPIModel model)
        {
            PantsAPIModel response = new PantsAPIModel();

            if (model.ID.HasValue)
            {
                Pants item = UoW.PantsRepository.GetFull(model.ID.Value);
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

            return new KendoResponse<PantsAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<PantsAPIModel> Delete(int id)
        {
            Pants item = UoW.PantsRepository.GetFull(id);

            if (item.ID != CurrentUser.ID)
            {

                if (item.PantsOctopi != null
                && item.PantsOctopi.Status == EquipmentStatusType.InUse)
                {
                    item.PantsOctopi.Status = EquipmentStatusType.Ready;
                }

                item.Status = EquipmentStatusType.Trash;
                item.PantsOctopi = null;
                UoW.Save();
            }

            return new KendoResponse<PantsAPIModel>()
            {
                Response = Convert(item)
            };
        }


        protected override Pants Bind(Pants item, PantsAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (model.PantsOctopiID.HasValue
            && (!item.PantsOctopiID.HasValue || (item.PantsOctopiID.HasValue && model.PantsOctopiID.Value != item.PantsOctopiID.Value)))
            {
                if (model.PantsOctopiID.Value == NoPantsOctopiID)
                {
                    if (item.PantsOctopi != null
                     && item.PantsOctopi.Status == EquipmentStatusType.InUse)
                    {
                        item.PantsOctopi.Status = EquipmentStatusType.Ready;
                    }
                    item.PantsOctopi = null;
                }
                else
                {
                    PantsOctopi pantsOctopi = UoW.PantsOctopiRepository.GetFull(model.PantsOctopiID.Value);
                    if (pantsOctopi.Pants.Count() > 0)
                    {
                        throw new Exception($"{i18n.Resources.PantsOctopi} {i18n.Resources.AlreadyUsed}");
                    }

                    item.PantsOctopi = pantsOctopi;
                    pantsOctopi.Status = EquipmentStatusType.InUse;
                }
            }

            item.Location = model.Location;
            item.QAStatus = model.QAStatus;
            item.Status = model.Status;
            item.Size = model.Size;

            return item;
        }

        protected override PantsAPIModel Convert(Pants item)
        {
            if (item == null)
            {
                return null;
            }

            return new PantsAPIModel()
            {
                ID = item.ID,
                IDView = item.IDView,
                Location = item.Location,
                QAStatus = item.QAStatus,
                Size = item.Size,
                Status = item.Status,
                PantsOctopi = item.PantsOctopi,
                PantsOctopiID = item.PantsOctopiID
            };
        }
    }
}