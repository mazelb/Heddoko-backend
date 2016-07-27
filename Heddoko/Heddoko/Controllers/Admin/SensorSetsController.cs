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
    [RoutePrefix("admin/api/sensorSets")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class SensorSetsController : BaseAdminController<SensorSet, SensorSetsAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        bool isDeleted = false;
        bool isUsed = false;

        public override KendoResponse<IEnumerable<SensorSetsAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<SensorSet> items = null;
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

                    items = UoW.SensorSetRepository.GetAvailable(usedID);
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
                        items = UoW.SensorSetRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                && !isUsed)
            {
                items = UoW.SensorSetRepository.All(isDeleted);
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<SensorSetsAPIModel> itemsDefault = new List<SensorSetsAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new SensorSetsAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<SensorSetsAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<SensorSetsAPIModel> Get(int id)
        {
            SensorSet item = UoW.SensorSetRepository.Get(id);

            return new KendoResponse<SensorSetsAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<SensorSetsAPIModel> Post(SensorSetsAPIModel model)
        {
            SensorSetsAPIModel response;

            if (ModelState.IsValid)
            {
                SensorSet item = new SensorSet();

                Bind(item, model);

                UoW.SensorSetRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<SensorSetsAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<SensorSetsAPIModel> Put(SensorSetsAPIModel model)
        {
            SensorSetsAPIModel response = new SensorSetsAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<SensorSetsAPIModel>
                {
                    Response = response
                };
            }

            SensorSet item = UoW.SensorSetRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<SensorSetsAPIModel>
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

            return new KendoResponse<SensorSetsAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<SensorSetsAPIModel> Delete(int id)
        {
            SensorSet item = UoW.SensorSetRepository.Get(id);

            if (item.ID == CurrentUser.ID)
            {
                return new KendoResponse<SensorSetsAPIModel>
                {
                    Response = Convert(item)
                };
            }

            UoW.Save();

            return new KendoResponse<SensorSetsAPIModel>
            {
                Response = Convert(item)
            };
        }

        protected override SensorSet Bind(SensorSet item, SensorSetsAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            //TODO: BENB - add these later
            //item.IDView = model.IDView
            //item.Sensors = model.sensorIDs;
            //item.Kit = model.KitID;
            item.QAStatus = model.QAStatus;
            
            return item;
        }

        protected override SensorSetsAPIModel Convert(SensorSet item)
        {
            if (item == null)
            {
                return null;
            }

            return new SensorSetsAPIModel
            {
                ID = item.ID,
                QAStatus = item.QAStatus,
                //KitID = item.KitID
            };
        }
    }
}