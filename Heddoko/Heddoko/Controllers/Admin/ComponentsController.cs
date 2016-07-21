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

namespace Heddoko.Controllers.Admin
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/components")]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public class ComponentsController : BaseAdminController<Component, ComponentsAPIModel>
    {
        const string Search = "Search";
        const string IsDeleted = "IsDeleted";
        const string Used = "Used";

        public override KendoResponse<IEnumerable<ComponentsAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<Component> items = null;
            int count = 0;

            bool isDeleted = false;
            bool isUsed = false;

            if(request != null && request.Filter != null)
            {
                KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);

                if(isDeletedFilter != null)
                {
                    isDeleted = true;
                }

                KendoFilterItem searchFilter = request.Filter.Get(Search);
                if (searchFilter != null
                    && !string.IsNullOrEmpty(searchFilter.Value))
                {
                    items = UoW.ComponentRepository.Search(searchFilter.Value, isDeleted);
                }
            }

            if (items == null)
            {
                items = UoW.ComponentRepository.All(isDeleted);
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

            return new KendoResponse<IEnumerable<ComponentsAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<ComponentsAPIModel> Get(int id)
        {
            Component item = UoW.ComponentRepository.Get(id);

            return new KendoResponse<ComponentsAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<ComponentsAPIModel> Post(ComponentsAPIModel model)
        {
            ComponentsAPIModel response = new ComponentsAPIModel();

            if (ModelState.IsValid)
            {
                Component item = new Component();

                Bind(item, model);

                UoW.ComponentRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<ComponentsAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<ComponentsAPIModel> Put(ComponentsAPIModel model)
        {
            ComponentsAPIModel response = new ComponentsAPIModel();

            if(model.ID.HasValue)
            {
                Component item = UoW.ComponentRepository.GetFull(model.ID.Value);
                if (item != null)
                {
                    if(ModelState.IsValid)
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

            return new KendoResponse<ComponentsAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<ComponentsAPIModel> Delete(int id)
        {
            Component item = UoW.ComponentRepository.GetFull(id);

            if (item.ID != CurrentUser.ID)
            {
                item.Status = EquipmentStatusType.Trash;

                UoW.Save();
            }

            return new KendoResponse<ComponentsAPIModel>()
            {
                Response = Convert(item)
            };
        }

        protected override Component Bind(Component item, ComponentsAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Type = model.Type;
            item.Status = model.Status;
            item.Quantity = model.Quantity;

            return item;
        }

        protected override ComponentsAPIModel Convert(Component item)
        {
            if (item == null)
            {
                return null;
            }

            return new ComponentsAPIModel()
            {
                ID = item.ID,
                Status = item.Status,
                Type = item.Type,
                Quantity = item.Quantity
            };
        }
    }
}