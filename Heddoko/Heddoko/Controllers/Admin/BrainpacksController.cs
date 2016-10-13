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
    [RoutePrefix("admin/api/brainpacks")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class BrainpacksController : BaseAdminController<Brainpack, BrainpackAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";
        private const int NoPowerboardID = 0;
        private const int NoDataboardID = 0;

        public BrainpacksController() { }

        public BrainpacksController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<BrainpackAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Brainpack> items = null;

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

                    items = UoW.BrainpackRepository.GetAvailable(usedID);
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
                        items = UoW.BrainpackRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.BrainpackRepository.All(isDeleted);
            }

            int count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<BrainpackAPIModel> itemsDefault = new List<BrainpackAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new BrainpackAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<BrainpackAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<BrainpackAPIModel> Get(int id)
        {
            Brainpack item = UoW.BrainpackRepository.Get(id);

            return new KendoResponse<BrainpackAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<BrainpackAPIModel> Post(BrainpackAPIModel model)
        {
            BrainpackAPIModel response;

            if (ModelState.IsValid)
            {
                Brainpack item = new Brainpack();

                Bind(item, model);

                UoW.BrainpackRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<BrainpackAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<BrainpackAPIModel> Put(BrainpackAPIModel model)
        {
            BrainpackAPIModel response = new BrainpackAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<BrainpackAPIModel>
                {
                    Response = response
                };
            }


            Brainpack item = UoW.BrainpackRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<BrainpackAPIModel>
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

            return new KendoResponse<BrainpackAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<BrainpackAPIModel> Delete(int id)
        {
            Brainpack item = UoW.BrainpackRepository.Get(id);

            if (item.Id != CurrentUser.Id)
            {
                item.Status = EquipmentStatusType.Trash;
                UoW.KitRepository.RemoveBrainpack(item.Id);

                UoW.Save();
            }

            return new KendoResponse<BrainpackAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            List<HistoryNotes> item = UoW.BrainpackRepository.HistoryNotes(id);

            return new KendoResponse<IEnumerable<HistoryNotes>>
            {
                Response = item
            };
        }

        protected override Brainpack Bind(Brainpack item, BrainpackAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (model.FirmwareID.HasValue)
            {
                item.Firmware = UoW.FirmwareRepository.Get(model.FirmwareID.Value);
            }

            if (model.PowerboardID.HasValue
                &&
                (!item.PowerboardID.HasValue || (model.PowerboardID.Value != item.PowerboardID.Value)))
            {
                if (model.PowerboardID.Value == NoPowerboardID)
                {
                    if (item.Powerboard != null
                        &&
                        item.Powerboard.Status == EquipmentStatusType.InUse)
                    {
                        item.Powerboard.Status = EquipmentStatusType.Ready;
                    }
                    item.Powerboard = null;
                }
                else
                {
                    Powerboard powerboard = UoW.PowerboardRepository.GetFull(model.PowerboardID.Value);

                    if (powerboard.Brainpack != null)
                    {
                        throw new Exception($"{Resources.Powerboard} {Resources.AlreadyUsed}");
                    }

                    item.Powerboard = powerboard;
                    powerboard.Status = EquipmentStatusType.InUse;
                }
            }

            if (model.DataboardID.HasValue
               &&
               (!item.DataboardID.HasValue || (model.DataboardID.Value != item.DataboardID.Value)))
            {
                if (model.DataboardID.Value == NoDataboardID)
                {
                    if (item.Databoard != null
                        &&
                        item.Databoard.Status == EquipmentStatusType.InUse)
                    {
                        item.Databoard.Status = EquipmentStatusType.Ready;
                    }
                    item.Databoard = null;
                }
                else
                {
                    Databoard databoard = UoW.DataboardRepository.GetFull(model.DataboardID.Value);

                    if (databoard.Brainpack != null)
                    {
                        throw new Exception($"{Resources.Databoard} {Resources.AlreadyUsed}");
                    }

                    item.Databoard = databoard;
                    databoard.Status = EquipmentStatusType.InUse;
                }
            }

            item.Version = model.Version?.Trim(); ;
            item.Status = model.Status;
            item.Location = model.Location?.Trim(); ;
            item.Notes = model.Notes?.Trim();
            item.Label = model.Label?.Trim();

            if (model.QaStatuses != null)
            {
                item.QAStatus = BrainpackQAStatusType.None;
                foreach (var qaStatus in model.QaStatuses)
                {
                    if (qaStatus.Value)
                    {
                        BrainpackQAStatusType status = qaStatus.Key.ParseEnum<BrainpackQAStatusType>(BrainpackQAStatusType.None);

                        if (status == BrainpackQAStatusType.None || status == BrainpackQAStatusType.TestedAndReady)
                        {
                            continue;
                        }

                        if (item.QAStatus == BrainpackQAStatusType.None)
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
                item.QAStatus = BrainpackQAStatusType.None;
            }

            return item;
        }


        protected override BrainpackAPIModel Convert(Brainpack item)
        {
            if (item == null)
            {
                return null;
            }

            return new BrainpackAPIModel
            {
                ID = item.Id,
                IDView = item.IDView,
                Version = item.Version,
                Location = item.Location,
                Status = item.Status,
                FirmwareID = item.FirmwareID ?? 0,
                Firmware = item.Firmware,
                QAStatus = item.QAStatus,
                Powerboard = item.Powerboard,
                Databoard = item.Databoard,
                PowerboardID = item.PowerboardID ?? 0,
                DataboardID = item.DataboardID ?? 0,
                Label = item.Label,
                Notes = item.Notes
            };
        }
    }
}