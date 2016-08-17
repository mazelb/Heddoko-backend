using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/pants")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class PantsController : BaseAdminController<Pants, PantsAPIModel>
    {
        private const string Search = "Search";
        private const string Status = "Status";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";
        private const int NoPantsOctopiID = 0;

        public override KendoResponse<IEnumerable<PantsAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Pants> items = null;
            int count = 0;

            bool isDeleted = false;
            bool isUsed = false;

            if (request?.Filter != null)
            {
                KendoFilterItem isUsedFilter = request.Filter.Get(Used);

                if (isUsedFilter != null)
                {
                    int tmp = 0;
                    int? usedID = null;
                    if (int.TryParse(isUsedFilter.Value, out tmp))
                    {
                        usedID = tmp;
                    }

                    items = UoW.PantsRepository.GetAvailable(usedID);
                    isUsed = true;
                }
                else
                {
                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }

                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    KendoFilterItem statusFilter = request.Filter.Get(Status);
                    int? statusInt = null;
                    int temp;
                    if (!string.IsNullOrEmpty(statusFilter?.Value) && int.TryParse(statusFilter.Value, out temp))
                    {
                        statusInt = temp;
                    }
                    if (statusInt.HasValue || !string.IsNullOrEmpty(searchFilter?.Value))
                    {
                        items = UoW.PantsRepository.Search(searchFilter?.Value, statusInt, isDeleted);
                    }
                }
            }

            if (items == null)
            {
                items = UoW.PantsRepository.All(isDeleted);
            }

            count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<PantsAPIModel> itemsDefault = new List<PantsAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new PantsAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<PantsAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<PantsAPIModel> Get(int id)
        {
            Pants item = UoW.PantsRepository.Get(id);

            return new KendoResponse<PantsAPIModel>
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
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<PantsAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<PantsAPIModel> Put(PantsAPIModel model)
        {
            PantsAPIModel response = new PantsAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<PantsAPIModel>
                {
                    Response = response
                };
            }

            Pants item = UoW.PantsRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<PantsAPIModel>
                {
                    Response = response
                };
            }

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

            return new KendoResponse<PantsAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<PantsAPIModel> Delete(int id)
        {
            Pants item = UoW.PantsRepository.GetFull(id);

            if (item.ID == CurrentUser.ID)
            {
                return new KendoResponse<PantsAPIModel>
                {
                    Response = Convert(item)
                };
            }

            if (item.PantsOctopi != null
                &&
                item.PantsOctopi.Status == EquipmentStatusType.InUse)
            {
                item.PantsOctopi.Status = EquipmentStatusType.Ready;
            }

            item.Status = EquipmentStatusType.Trash;
            item.PantsOctopi = null;
            UoW.KitRepository.RemovePants(item.ID);
            UoW.Save();

            return new KendoResponse<PantsAPIModel>
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
                &&
                (!item.PantsOctopiID.HasValue || (model.PantsOctopiID.Value != item.PantsOctopiID.Value)))
            {
                if (model.PantsOctopiID.Value == NoPantsOctopiID)
                {
                    if (item.PantsOctopi != null
                        &&
                        item.PantsOctopi.Status == EquipmentStatusType.InUse)
                    {
                        item.PantsOctopi.Status = EquipmentStatusType.Ready;
                    }
                    item.PantsOctopi = null;
                }
                else
                {
                    PantsOctopi pantsOctopi = UoW.PantsOctopiRepository.GetFull(model.PantsOctopiID.Value);
                    if (pantsOctopi.Pants != null)
                    {
                        throw new Exception($"{Resources.PantsOctopi} {Resources.AlreadyUsed}");
                    }

                    item.PantsOctopi = pantsOctopi;
                    pantsOctopi.Status = EquipmentStatusType.InUse;
                }
            }

            item.Location = model.Location?.Trim(); ;
            item.QAStatus = model.QAStatus;
            item.Notes = model.Notes?.Trim();
            item.Label = model.Label?.Trim();
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

            return new PantsAPIModel
            {
                ID = item.ID,
                IDView = item.IDView,
                Location = item.Location,
                QAStatus = item.QAStatus,
                Size = item.Size,
                Status = item.Status,
                PantsOctopi = item.PantsOctopi,
                PantsOctopiID = item.PantsOctopiID ?? 0,
                Label = item.Label,
                Notes = item.Notes
            };
        }
    }
}