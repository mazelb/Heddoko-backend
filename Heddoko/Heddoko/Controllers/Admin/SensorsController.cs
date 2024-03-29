﻿/**
 * @file SensorsController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Hangfire;
using Heddoko.Helpers.DomainRouting.Http;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/Sensors")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class SensorsController : BaseAdminController<Sensor, SensorAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Status = "Status";
        private const string Used = "Used";
        private const string SensorSetID = "SensorSetID";
        private const string LinkField = "idView";

        public SensorsController() { }

        public SensorsController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<SensorAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Sensor> items = null;

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
                    isUsed = true;
                }
                else
                {
                    KendoFilterItem linkFieldFilter = request.Filter.Get(LinkField);
                    if (!string.IsNullOrEmpty(linkFieldFilter?.Value))
                    {
                        items = UoW.SensorRepository.SearchAvailable(linkFieldFilter.Value);
                    }

                    KendoFilterItem sensorSetIDFilter = request.Filter.Get(SensorSetID);
                    if (!string.IsNullOrEmpty(sensorSetIDFilter?.Value))
                    {
                        int sensorSetID;
                        if (int.TryParse(sensorSetIDFilter.Value, out sensorSetID))
                        {
                            items = UoW.SensorRepository.GetBySensorSet(sensorSetID);
                        }
                    }


                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }

                    //Setup Search
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
                        items = UoW.SensorRepository.Search(searchFilter?.Value, statusInt, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.SensorRepository.All(isDeleted);
            }

            int count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<SensorAPIModel> itemsDefault = new List<SensorAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new SensorAPIModel(true)
                {
                    ID = 0
                });

                count++; //Since we are adding something to the list
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<SensorAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<SensorAPIModel> Get(int id)
        {
            Sensor item = UoW.SensorRepository.Get(id);

            return new KendoResponse<SensorAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<SensorAPIModel> Post(SensorAPIModel model)
        {
            SensorAPIModel response;

            if (ModelState.IsValid)
            {
                Sensor item = new Sensor();

                Bind(item, model);

                UoW.SensorRepository.Create(item);

                BackgroundJob.Enqueue(() => Services.AssembliesManager.GetAssemblies(true));

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<SensorAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<SensorAPIModel> Put(SensorAPIModel model)
        {
            SensorAPIModel response = new SensorAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<SensorAPIModel>
                {
                    Response = response
                };
            }

            Sensor item = UoW.SensorRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<SensorAPIModel>
                {
                    Response = response
                };
            }

            if (ModelState.IsValid)
            {
                Bind(item, model);
                UoW.Save();

                BackgroundJob.Enqueue(() => Services.AssembliesManager.GetAssemblies(true));

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<SensorAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<SensorAPIModel> Delete(int id)
        {
            Sensor item = UoW.SensorRepository.Get(id);

            if (item.Id == CurrentUser.Id)
            {
                return new KendoResponse<SensorAPIModel>
                {
                    Response = Convert(item)
                };
            }

            item.Status = EquipmentStatusType.Trash;
            item.SensorSet = null;
            UoW.Save();

            return new KendoResponse<SensorAPIModel>
            {
                Response = Convert(item)
            };
        }

        [DomainRoute("{id:int}/unlink", Constants.ConfigKeyName.DashboardSite)]
        public KendoResponse<SensorAPIModel> Unlink(int id)
        {
            Sensor item = UoW.SensorRepository.GetFull(id);
            if (item == null)
            {
                return new KendoResponse<SensorAPIModel>()
                {
                    Response = null
                };
            }

            item.SensorSet = null;
            item.AnatomicalLocation = null;
            if (item.Status == EquipmentStatusType.InUse)
            {
                item.Status = EquipmentStatusType.Ready;

                BackgroundJob.Enqueue(() => Services.AssembliesManager.GetAssemblies(true));
            }
            UoW.Save();

            return new KendoResponse<SensorAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            List<HistoryNotes> item = UoW.SensorRepository.HistoryNotes(id);

            return new KendoResponse<IEnumerable<HistoryNotes>>
            {
                Response = item
            };
        }

        protected override Sensor Bind(Sensor item, SensorAPIModel model)
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

            item.Type = model.Type;
            item.Version = model.Version?.Trim();
            item.Status = model.Status;

            if (model.AnatomicalLocation.HasValue)
            {
                item.AnatomicalLocation = model.AnatomicalLocation.Value;
            }

            item.Location = model.Location?.Trim(); ;
            item.Notes = model.Notes?.Trim();
            item.Label = model.Label?.Trim();

            if (model.QaStatuses != null)
            {
                item.QAStatus = SensorQAStatusType.None;
                foreach (var qaStatus in model.QaStatuses)
                {
                    if (qaStatus.Value)
                    {
                        SensorQAStatusType status = qaStatus.Key.ParseEnum<SensorQAStatusType>(SensorQAStatusType.None);

                        if (status == SensorQAStatusType.None || status == SensorQAStatusType.TestedAndReady)
                        {
                            continue;
                        }

                        if (item.QAStatus == SensorQAStatusType.None)
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
                item.QAStatus = SensorQAStatusType.None;
            }

            item.SensorSet = model.SensorSetID.HasValue ? UoW.SensorSetRepository.Get(model.SensorSetID.Value) : null;

            return item;
        }

        protected override SensorAPIModel Convert(Sensor item)
        {
            if (item == null)
            {
                return null;
            }

            return new SensorAPIModel
            {
                ID = item.Id,
                IDView = item.IDView,
                Type = item.Type,
                Version = item.Version,
                Location = item.Location,
                FirmwareID = item.FirmwareID ?? 0,
                Status = item.Status,
                QAStatus = item.QAStatus,
                SensorSetID = item.SensorSetID ?? 0,
                SensorSet = item.SensorSet,
                AnatomicalLocation = item.AnatomicalLocation,
                Label = item.Label,
                Notes = item.Notes,
                Firmware = item.Firmware
            };
        }
    }
}