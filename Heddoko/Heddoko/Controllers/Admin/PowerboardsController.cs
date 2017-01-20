/**
 * @file PowerboardsController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
    [RoutePrefix("admin/api/powerboards")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class PowerboardsController : BaseAdminController<Powerboard, PowerboardAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        public PowerboardsController() { }

        public PowerboardsController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<PowerboardAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Powerboard> items = null;

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

                    items = UoW.PowerboardRepository.GetAvailable(usedID);
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
                        items = UoW.PowerboardRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.PowerboardRepository.All(isDeleted);
            }

            int count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<PowerboardAPIModel> itemsDefault = new List<PowerboardAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new PowerboardAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<PowerboardAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<PowerboardAPIModel> Get(int id)
        {
            Powerboard item = UoW.PowerboardRepository.Get(id);

            return new KendoResponse<PowerboardAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<PowerboardAPIModel> Post(PowerboardAPIModel model)
        {
            PowerboardAPIModel response;

            if (ModelState.IsValid)
            {
                Powerboard item = new Powerboard();

                Bind(item, model);

                UoW.PowerboardRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<PowerboardAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<PowerboardAPIModel> Put(PowerboardAPIModel model)
        {
            PowerboardAPIModel response = new PowerboardAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<PowerboardAPIModel>
                {
                    Response = response
                };
            }


            Powerboard item = UoW.PowerboardRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<PowerboardAPIModel>
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

            return new KendoResponse<PowerboardAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<PowerboardAPIModel> Delete(int id)
        {
            Powerboard item = UoW.PowerboardRepository.Get(id);

            if (item.Id != CurrentUser.Id)
            {
                item.Status = EquipmentStatusType.Trash;
                UoW.BrainpackRepository.RemovePowerboard(item.Id);

                UoW.Save();
            }

            return new KendoResponse<PowerboardAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            List<HistoryNotes> item = UoW.PowerboardRepository.HistoryNotes(id);

            return new KendoResponse<IEnumerable<HistoryNotes>>
            {
                Response = item
            };
        }

        protected override Powerboard Bind(Powerboard item, PowerboardAPIModel model)
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
                item.QAStatus = PowerboardQAStatusType.None;
                foreach (var qaStatus in model.QaStatuses)
                {
                    if (qaStatus.Value)
                    {
                        PowerboardQAStatusType status = qaStatus.Key.ParseEnum<PowerboardQAStatusType>(PowerboardQAStatusType.None);

                        if (status == PowerboardQAStatusType.None || status == PowerboardQAStatusType.TestedAndReady)
                        {
                            continue;
                        }

                        if (item.QAStatus == PowerboardQAStatusType.None)
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
                item.QAStatus = PowerboardQAStatusType.None;
            }

            return item;
        }

        protected override PowerboardAPIModel Convert(Powerboard item)
        {
            if (item == null)
            {
                return null;
            }

            return new PowerboardAPIModel
            {
                ID = item.Id,
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