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
    [RoutePrefix("admin/api/shirts")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class ShirtsController : BaseAdminController<Shirt, ShirtAPIModel>
    {
        private const string Search = "Search";
        private const string Status = "Status";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";
        private const int NoShirtsOctopiID = 0;

        public override KendoResponse<IEnumerable<ShirtAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Shirt> items = null;
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

                    items = UoW.ShirtRepository.GetAvailable(usedID);
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
                        items = UoW.ShirtRepository.Search(searchFilter?.Value, statusInt, isDeleted);
                    }       
                }
            }

            if (items == null)
            {
                items = UoW.ShirtRepository.All(isDeleted);
            }

            count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<ShirtAPIModel> itemsDefault = new List<ShirtAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new ShirtAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<ShirtAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<ShirtAPIModel> Get(int id)
        {
            Shirt item = UoW.ShirtRepository.Get(id);

            return new KendoResponse<ShirtAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<ShirtAPIModel> Post(ShirtAPIModel model)
        {
            ShirtAPIModel response;

            if (ModelState.IsValid)
            {
                Shirt item = new Shirt();

                Bind(item, model);

                UoW.ShirtRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<ShirtAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<ShirtAPIModel> Put(ShirtAPIModel model)
        {
            ShirtAPIModel response = new ShirtAPIModel();

            if (model.ID.HasValue)
            {
                Shirt item = UoW.ShirtRepository.GetFull(model.ID.Value);
                if (item == null)
                {
                    return new KendoResponse<ShirtAPIModel>
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
            }

            return new KendoResponse<ShirtAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<ShirtAPIModel> Delete(int id)
        {
            Shirt item = UoW.ShirtRepository.GetFull(id);

            if (item.ID == CurrentUser.ID)
            {
                return new KendoResponse<ShirtAPIModel>
                {
                    Response = Convert(item)
                };
            }

            if (item.ShirtOctopi != null
                &&
                item.ShirtOctopi.Status == EquipmentStatusType.InUse)
            {
                item.ShirtOctopi.Status = EquipmentStatusType.Ready;
            }

            item.Status = EquipmentStatusType.Trash;
            item.ShirtOctopi = null;
            UoW.KitRepository.RemoveShirt(item.ID);
            UoW.Save();

            return new KendoResponse<ShirtAPIModel>
            {
                Response = Convert(item)
            };
        }

        protected override Shirt Bind(Shirt item, ShirtAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (model.ShirtOctopiID.HasValue
                &&
                (!item.ShirtOctopiID.HasValue || (model.ShirtOctopiID.Value != item.ShirtOctopiID.Value)))
            {
                if (model.ShirtOctopiID.Value == NoShirtsOctopiID)
                {
                    if (item.ShirtOctopi != null
                        &&
                        item.ShirtOctopi.Status == EquipmentStatusType.InUse)
                    {
                        item.ShirtOctopi.Status = EquipmentStatusType.Ready;
                    }
                    item.ShirtOctopi = null;
                }
                else
                {
                    ShirtOctopi shirtOctopi = UoW.ShirtOctopiRepository.GetFull(model.ShirtOctopiID.Value);
                    if (shirtOctopi.Size != item.Size)
                    {
                        throw new Exception($"{Resources.ShirtsOctopi} {Resources.WrongSize}");
                    }
                    if (shirtOctopi.Shirt.Any())
                    {
                        throw new Exception($"{Resources.ShirtsOctopi} {Resources.AlreadyUsed}");
                    }

                    item.ShirtOctopi = shirtOctopi;
                    shirtOctopi.Status = EquipmentStatusType.InUse;
                }
            }

            item.Location = model.Location?.Trim(); ;
            item.Notes = model.Notes?.Trim();
            item.Label = model.Label?.Trim();
            item.Status = model.Status;
            item.Size = model.Size;

            if (model.QaStatuses != null)
            {
                item.QAStatus = ShirtQAStatusType.None;
                foreach (var qaStatus in model.QaStatuses)
                {
                    if (qaStatus.Value)
                    {
                        ShirtQAStatusType status = qaStatus.Key.ParseEnum<ShirtQAStatusType>(ShirtQAStatusType.None);

                        if (status == ShirtQAStatusType.None || status == ShirtQAStatusType.TestedAndReady)
                        {
                            continue;
                        }

                        if (item.QAStatus == ShirtQAStatusType.None)
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
                item.QAStatus = ShirtQAStatusType.None;
            }

            return item;
        }

        protected override ShirtAPIModel Convert(Shirt item)
        {
            if (item == null)
            {
                return null;
            }

            return new ShirtAPIModel
            {
                ID = item.ID,
                IDView = item.IDView,
                Location = item.Location,
                QAStatus = item.QAStatus,
                Size = item.Size,
                Status = item.Status,
                ShirtOctopi = item.ShirtOctopi,
                ShirtOctopiID = item.ShirtOctopiID ?? 0,
                Label = item.Label,
                Notes = item.Notes
            };
        }
    }
}