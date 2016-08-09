using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/pantsOctopi")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class PantsOctopiController : BaseAdminController<PantsOctopi, PantsOctopiAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";


        public override KendoResponse<IEnumerable<PantsOctopiAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<PantsOctopi> items = null;
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

                    items = UoW.PantsOctopiRepository.GetAvailable(usedID);
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
                    if (!string.IsNullOrEmpty(searchFilter?.Value))
                    {
                        items = UoW.PantsOctopiRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.PantsOctopiRepository.All(isDeleted);
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<PantsOctopiAPIModel> itemsDefault = new List<PantsOctopiAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new PantsOctopiAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<PantsOctopiAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<PantsOctopiAPIModel> Get(int id)
        {
            PantsOctopi item = UoW.PantsOctopiRepository.Get(id);

            return new KendoResponse<PantsOctopiAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<PantsOctopiAPIModel> Post(PantsOctopiAPIModel model)
        {
            PantsOctopiAPIModel response;

            if (ModelState.IsValid)
            {
                PantsOctopi item = new PantsOctopi();

                Bind(item, model);

                UoW.PantsOctopiRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<PantsOctopiAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<PantsOctopiAPIModel> Put(PantsOctopiAPIModel model)
        {
            PantsOctopiAPIModel response = new PantsOctopiAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<PantsOctopiAPIModel>
                {
                    Response = response
                };
            }

            PantsOctopi item = UoW.PantsOctopiRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<PantsOctopiAPIModel>
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

            return new KendoResponse<PantsOctopiAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<PantsOctopiAPIModel> Delete(int id)
        {
            PantsOctopi item = UoW.PantsOctopiRepository.Get(id);

            if (item.ID == CurrentUser.ID)
            {
                return new KendoResponse<PantsOctopiAPIModel>
                {
                    Response = Convert(item)
                };
            }

            item.Status = EquipmentStatusType.Trash;

            UoW.PantsRepository.RemovePantsOctopi(item.ID);

            UoW.Save();

            return new KendoResponse<PantsOctopiAPIModel>
            {
                Response = Convert(item)
            };
        }


        protected override PantsOctopi Bind(PantsOctopi item, PantsOctopiAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Location = model.Location.Trim(); ;
            item.QAStatus = model.QAStatus;
            item.Notes = model.Notes.Trim();
            item.Label = model.Label?.Trim();
            item.Status = model.Status;
            item.Size = model.Size;

            return item;
        }

        protected override PantsOctopiAPIModel Convert(PantsOctopi item)
        {
            if (item == null)
            {
                return null;
            }

            return new PantsOctopiAPIModel
            {
                ID = item.ID,
                IDView = item.IDView,
                Location = item.Location,
                QAStatus = item.QAStatus,
                Size = item.Size,
                Status = item.Status,
                Label = item.Label,
                Notes = item.Notes
            };
        }
    }
}