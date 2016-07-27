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
    [RoutePrefix("admin/api/Sensors")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class SensorsController : BaseAdminController<Sensor, SensorsAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";  

        public override KendoResponse<IEnumerable<SensorsAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Sensor> items = null;
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

                    items = UoW.SensorRepository.GetAvailable(usedID);
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
                        items = UoW.SensorRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                && !isUsed)
            {
                items = UoW.SensorRepository.All(isDeleted);
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<SensorsAPIModel> itemsDefault = new List<SensorsAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new SensorsAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<SensorsAPIModel>>
            {   
                Response = itemsDefault,
                Total = count
            };
        }
        
        public override KendoResponse<SensorsAPIModel> Get(int id)
        {
            Sensor item = UoW.SensorRepository.Get(id);

            return new KendoResponse<SensorsAPIModel>
            {
                Response = Convert(item)
            };
        }
        
        public override KendoResponse<SensorsAPIModel> Post(SensorsAPIModel model)
        {
            SensorsAPIModel response;

            if (ModelState.IsValid)
            {
                Sensor item = new Sensor();

                Bind(item, model);

                UoW.SensorRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<SensorsAPIModel>
            {
                Response = response
            };
        }
        
        public override KendoResponse<SensorsAPIModel> Put(SensorsAPIModel model)
        {
            SensorsAPIModel response = new SensorsAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<SensorsAPIModel>
                {
                    Response = response
                };
            }

            Sensor item = UoW.SensorRepository.GetFull(model.ID.Value);
            if(item == null)
            {
                return new KendoResponse<SensorsAPIModel>
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

            return new KendoResponse<SensorsAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<SensorsAPIModel> Delete(int id)
        {
            Sensor item = UoW.SensorRepository.Get(id);

            if (item.ID == CurrentUser.ID)
            {
                return new KendoResponse<SensorsAPIModel>
                {
                    Response = Convert(item)
                };
            }

            item.Status = EquipmentStatusType.Trash;

            UoW.Save();

            return new KendoResponse<SensorsAPIModel>
            {
                Response = Convert(item)
            };
        }

        protected override Sensor Bind(Sensor item, SensorsAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Type = model.Type;
            item.Version = model.version;
            item.Location = model.Location;
            //item.FirmwareID = model.FirmwareID;
            item.Status = model.Status;
            item.QAStatus = model.QAStatus;
            //item.SensorSetID = model.SetID;
            item.AnatomicLocation = model.AnatomicalPosition;

            return item;
        }

        protected override SensorsAPIModel Convert(Sensor item)
        {
            if (item == null)
            {
                return null;
            }

            return new SensorsAPIModel
            {
                ID = item.ID,
                Type = item.Type,
                version = item.Version,
                Location = item.Location,
                //FirmwareID = item.FirmwareID,
                Status = item.Status,
                QAStatus = item.QAStatus,
                //SetID = item.SensorSetID,
                AnatomicalPosition = item.AnatomicLocation
            };
        }
    }
}