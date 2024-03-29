﻿/**
 * @file ShirtsOctopiController.cs
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
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/shirtsOctopi")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class ShirtsOctopiController : BaseAdminController<ShirtOctopi, ShirtOctopiAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        public ShirtsOctopiController() { }

        public ShirtsOctopiController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<ShirtOctopiAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<ShirtOctopi> items = null;
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

                    items = UoW.ShirtOctopiRepository.GetAvailable(usedID);
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
                        items = UoW.ShirtOctopiRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.ShirtOctopiRepository.All(isDeleted);
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<ShirtOctopiAPIModel> itemsDefault = new List<ShirtOctopiAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new ShirtOctopiAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<ShirtOctopiAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<ShirtOctopiAPIModel> Get(int id)
        {
            ShirtOctopi item = UoW.ShirtOctopiRepository.Get(id);

            return new KendoResponse<ShirtOctopiAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<ShirtOctopiAPIModel> Post(ShirtOctopiAPIModel model)
        {
            ShirtOctopiAPIModel response;

            if (ModelState.IsValid)
            {
                ShirtOctopi item = new ShirtOctopi();

                Bind(item, model);

                UoW.ShirtOctopiRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<ShirtOctopiAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<ShirtOctopiAPIModel> Put(ShirtOctopiAPIModel model)
        {
            ShirtOctopiAPIModel response = new ShirtOctopiAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<ShirtOctopiAPIModel>
                {
                    Response = response
                };
            }

            ShirtOctopi item = UoW.ShirtOctopiRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<ShirtOctopiAPIModel>
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

            return new KendoResponse<ShirtOctopiAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<ShirtOctopiAPIModel> Delete(int id)
        {
            ShirtOctopi item = UoW.ShirtOctopiRepository.Get(id);

            if (item.Id == CurrentUser.Id)
            {
                return new KendoResponse<ShirtOctopiAPIModel>
                {
                    Response = Convert(item)
                };
            }

            item.Status = EquipmentStatusType.Trash;

            UoW.ShirtRepository.RemoveShirtOctopi(item.Id);

            UoW.Save();

            return new KendoResponse<ShirtOctopiAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            List<HistoryNotes> item = UoW.ShirtOctopiRepository.HistoryNotes(id);

            return new KendoResponse<IEnumerable<HistoryNotes>>
            {
                Response = item
            };
        }

        protected override ShirtOctopi Bind(ShirtOctopi item, ShirtOctopiAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Location = model.Location?.Trim();
            item.Notes = model.Notes?.Trim();
            item.Label = model.Label?.Trim();
            item.Status = model.Status;
            item.Size = model.Size;

            if (model.QaStatuses != null)
            {
                item.QAStatus = ShirtOctopiQAStatusType.None;
                foreach (var qaStatus in model.QaStatuses)
                {
                    if (qaStatus.Value)
                    {
                        ShirtOctopiQAStatusType status = qaStatus.Key.ParseEnum<ShirtOctopiQAStatusType>(ShirtOctopiQAStatusType.None);

                        if (status == ShirtOctopiQAStatusType.None || status == ShirtOctopiQAStatusType.TestedAndReady)
                        {
                            continue;
                        }

                        if (item.QAStatus == ShirtOctopiQAStatusType.None)
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
                item.QAStatus = ShirtOctopiQAStatusType.None;
            }

            return item;
        }

        protected override ShirtOctopiAPIModel Convert(ShirtOctopi item)
        {
            if (item == null)
            {
                return null;
            }

            return new ShirtOctopiAPIModel
            {
                ID = item.Id,
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