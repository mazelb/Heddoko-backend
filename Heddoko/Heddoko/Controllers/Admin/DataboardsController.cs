using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using Newtonsoft.Json;
using Services;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/databoards")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class DataboardsController : BaseAdminController<Databoard, DataboardAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        public override KendoResponse<IEnumerable<DataboardAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Databoard> items = null;

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

                    items = UoW.DataboardRepository.GetAvailable(usedID);
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
                        items = UoW.DataboardRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.DataboardRepository.All(isDeleted);
            }

            int count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<DataboardAPIModel> itemsDefault = new List<DataboardAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new DataboardAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<DataboardAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<DataboardAPIModel> Get(int id)
        {
            Databoard item = UoW.DataboardRepository.Get(id);

            return new KendoResponse<DataboardAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<DataboardAPIModel> Post(DataboardAPIModel model)
        {
            DataboardAPIModel response;

            if (ModelState.IsValid)
            {
                Databoard item = new Databoard();

                Bind(item, model);

                UoW.DataboardRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<DataboardAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<DataboardAPIModel> Put(DataboardAPIModel model)
        {
            DataboardAPIModel response = new DataboardAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<DataboardAPIModel>
                {
                    Response = response
                };
            }


            Databoard item = UoW.DataboardRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<DataboardAPIModel>
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

            return new KendoResponse<DataboardAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<DataboardAPIModel> Delete(int id)
        {
            Databoard item = UoW.DataboardRepository.Get(id);

            if (item.ID != CurrentUser.ID)
            {
                item.Status = EquipmentStatusType.Trash;
                UoW.BrainpackRepository.RemoveDataboard(item.ID);

                UoW.Save();
            }

            return new KendoResponse<DataboardAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            List<HistoryNotes> item = UoW.DataboardRepository.HistoryNotes(id);

            return new KendoResponse<IEnumerable<HistoryNotes>>
            {
                Response = item
            };
        }

        protected override Databoard Bind(Databoard item, DataboardAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (model.FirmwareID.HasValue)
            {
                item.Firmware = UoW.FirmwareRepository.Get(model.FirmwareID.Value);
            }
            else
            {
                item.Firmware = null;
            }

            item.Version = model.Version?.Trim();
            item.Status = model.Status;
            item.Location = model.Location?.Trim(); ;
            item.Notes = model.Notes?.Trim();
            item.Label = model.Label?.Trim();

            if (model.QaStatuses != null)
            {
                item.QAStatus = DataboardQAStatusType.None;
                foreach (var qaStatus in model.QaStatuses)
                {
                    if (qaStatus.Value)
                    {
                        DataboardQAStatusType status = qaStatus.Key.ParseEnum<DataboardQAStatusType>(DataboardQAStatusType.None);

                        if (status == DataboardQAStatusType.None || status == DataboardQAStatusType.TestedAndReady)
                        {
                            continue;
                        }

                        if (item.QAStatus == DataboardQAStatusType.None)
                        {
                            item.QAStatus = status;
                        }
                        else
                        {
                            item.QAStatus |= status;
                        }
                    }
                }
            }
            else
            {
                item.QAStatus = DataboardQAStatusType.None;
            }

            return item;
        }

        protected override DataboardAPIModel Convert(Databoard item)
        {
            if (item == null)
            {
                return null;
            }

            return new DataboardAPIModel
            {
                ID = item.ID,
                IDView = item.IDView,
                Version = item.Version,
                Location = item.Location,
                Status = item.Status,
                FirmwareID = item.FirmwareID ?? 0,
                Firmware = item.Firmware,
                Label = item.Label,
                Notes = item.Notes,
                QAStatus = item.QAStatus
            };
        }
    }
}