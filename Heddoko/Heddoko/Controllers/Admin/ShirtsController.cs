﻿using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/shirts")]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public class ShirtsController : BaseAdminController<Shirt, ShirtAPIModel>
    {
        const string Search = "Search";
        const string IsDeleted = "IsDeleted";
        const string Used = "Used";
        const int NoShirtsOctopiID = 0;

        public override KendoResponse<IEnumerable<ShirtAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<Shirt> items = null;
            int count = 0;

            bool isDeleted = false;

            if (request != null)
            {
                if (request.Filter != null)
                {
                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }

                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    if (searchFilter != null
                    && !string.IsNullOrEmpty(searchFilter.Value))
                    {
                        items = UoW.ShirtRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null)
            {

                items = UoW.ShirtRepository.All(isDeleted);
            }

            count = items.Count();

            if (request != null
             && request.Take.HasValue)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);

            }

            var result = items.ToList()
                              .Select(c => Convert(c));

            return new KendoResponse<IEnumerable<ShirtAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }
        public override KendoResponse<ShirtAPIModel> Get(int id)
        {
            Shirt item = UoW.ShirtRepository.Get(id);

            return new KendoResponse<ShirtAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<ShirtAPIModel> Post(ShirtAPIModel model)
        {
            ShirtAPIModel response = new ShirtAPIModel();

            if (ModelState.IsValid)
            {
                Shirt item = new Shirt();

                Bind(item, model);

                UoW.ShirtRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<ShirtAPIModel>()
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
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {

                        Bind(item, model);
                        UoW.Save();

                        response = Convert(item);
                    }
                    else
                    {
                        throw new ModelStateException()
                        {
                            ModelState = ModelState
                        };
                    }
                }
            }

            return new KendoResponse<ShirtAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<ShirtAPIModel> Delete(int id)
        {
            Shirt item = UoW.ShirtRepository.GetFull(id);

            if (item.ID != CurrentUser.ID)
            {

                if (item.ShirtOctopi != null
                && item.ShirtOctopi.Status == EquipmentStatusType.InUse)
                {
                    item.ShirtOctopi.Status = EquipmentStatusType.Ready;
                }

                item.Status = EquipmentStatusType.Trash;
                item.ShirtOctopi = null;
                UoW.Save();
            }

            return new KendoResponse<ShirtAPIModel>()
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
            && (!item.ShirtOctopiID.HasValue || (item.ShirtOctopiID.HasValue && model.ShirtOctopiID.Value != item.ShirtOctopiID.Value)))
            {
                if (model.ShirtOctopiID.Value == NoShirtsOctopiID)
                {
                    if (item.ShirtOctopi != null
                     && item.ShirtOctopi.Status == EquipmentStatusType.InUse)
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
                        throw new Exception($"{i18n.Resources.ShirtsOctopi} {i18n.Resources.WrongSize}");
                    }
                    if (shirtOctopi.Shirt.Count() > 0)
                    {
                        throw new Exception($"{i18n.Resources.ShirtsOctopi} {i18n.Resources.AlreadyUsed}");
                    }

                    item.ShirtOctopi = shirtOctopi;
                    shirtOctopi.Status = EquipmentStatusType.InUse;
                }
            }

            item.Location = model.Location;
            item.QAStatus = model.QAStatus;
            item.Status = model.Status;
            item.Size = model.Size;

            return item;
        }

        protected override ShirtAPIModel Convert(Shirt item)
        {
            if (item == null)
            {
                return null;
            }

            return new ShirtAPIModel()
            {
                ID = item.ID,
                IDView = item.IDView,
                Location = item.Location,
                QAStatus = item.QAStatus,
                Size = item.Size,
                Status = item.Status,
                ShirtOctopi = item.ShirtOctopi,
                ShirtOctopiID = item.ShirtOctopiID
            };
        }
    }
}