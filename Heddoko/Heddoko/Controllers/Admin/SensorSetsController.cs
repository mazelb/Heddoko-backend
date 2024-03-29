﻿/**
 * @file SensorSetsController.cs
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
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/sensorSets")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class SensorSetsController : BaseAdminController<SensorSet, SensorSetsAPIModel>
    {
        private const string Search = "Search";
        private const string Status = "Status";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        public SensorSetsController() { }

        public SensorSetsController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<SensorSetsAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<SensorSet> items = null;

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
                        items = UoW.SensorSetRepository.Search(searchFilter?.Value, statusInt, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.SensorSetRepository.All(isDeleted);
            }

            int count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
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

            if (item.Id == CurrentUser.Id)
            {
                return new KendoResponse<SensorSetsAPIModel>
                {
                    Response = Convert(item)
                };
            }
            UoW.SensorRepository.RemoveSensorSet(item.Id);
            UoW.KitRepository.RemoveSensorSet(item.Id);

            item.Status = EquipmentStatusType.Trash;
            UoW.Save();

            return new KendoResponse<SensorSetsAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            List<HistoryNotes> item = UoW.SensorSetRepository.HistoryNotes(id);

            return new KendoResponse<IEnumerable<HistoryNotes>>
            {
                Response = item
            };
        }

        protected override SensorSet Bind(SensorSet item, SensorSetsAPIModel model)
        {
            if (model == null)
            {
                return null;
            }


            item.Location = model.Location?.Trim(); ;
            item.QAStatus = model.QAStatus;
            item.Notes = model.Notes?.Trim();
            item.Label = model.Label?.Trim();
            item.Status = model.Status;

            if (model.Sensors == null)
            {
                return item;
            }

            if (model.Sensors != null)
            {
                if (item.Sensors == null)
                {
                    item.Sensors = new List<Sensor>();
                }

                foreach (int sensor in model.Sensors)
                {
                    if (item.Sensors.Any(c => c.Id == sensor))
                    {
                        continue;
                    }

                    Sensor sens = UoW.SensorRepository.Get(sensor);

                    if (sens != null
                        &&
                        !sens.SensorSetID.HasValue)
                    {

                        if (sens.Status == EquipmentStatusType.Ready)
                        {
                            sens.Status = EquipmentStatusType.InUse;
                        }
                        item.Sensors.Add(sens);
                    }
                }
            }

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
                ID = item.Id,
                IDView = item.IDView,
                QAStatus = item.QAStatus,
                Kit = item.Kit,
                KitID = item.Kit?.Id ?? 0,
                Status = item.Status,
                Label = item.Label,
                Notes = item.Notes,
                Location = item.Location
            };
        }
    }
}