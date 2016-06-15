using DAL.Models;
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
    [RoutePrefix("admin/api/materialTypes")]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public class MaterialTypesController : BaseAdminController<MaterialType, MaterialTypeAPIModel>
    {
        const string Search = "Search";

        public override KendoResponse<IEnumerable<MaterialTypeAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<MaterialType> items = null;
            int count = 0;
            if (request != null)
            {
                if (request.Filter != null)
                {
                    switch (request.Filter.Filters.Count())
                    {

                        case 1:
                            KendoFilterItem searchFilter = request.Filter.Get(Search);
                            if(searchFilter != null
                            && !string.IsNullOrEmpty(searchFilter.Value))
                            {
                                items = UoW.MaterialTypeRepository.Search(searchFilter.Value);
                            }
                            break;
                    }
                }
            }

            if (items == null)
            {
                items = UoW.MaterialTypeRepository.All();
            }

            count = items.Count();

            if (request != null)
            {
                if (request.Take.HasValue)
                {
                    items = items.Skip(request.Skip.Value)
                                 .Take(request.Take.Value);

                }
            }

            var result = items.ToList()
                              .Select(c => Convert(c));

            return new KendoResponse<IEnumerable<MaterialTypeAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<MaterialTypeAPIModel> Get(int id)
        {
            MaterialType item = UoW.MaterialTypeRepository.Get(id);

            return new KendoResponse<MaterialTypeAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<MaterialTypeAPIModel> Post(MaterialTypeAPIModel model)
        {
            MaterialTypeAPIModel response = new MaterialTypeAPIModel();

            if (ModelState.IsValid)
            {
                MaterialType item = new MaterialType();
                Bind(item, model);
                UoW.MaterialTypeRepository.Create(item);
                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<MaterialTypeAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<MaterialTypeAPIModel> Put(MaterialTypeAPIModel model)
        {
            MaterialTypeAPIModel response = new MaterialTypeAPIModel();

            if (model.ID.HasValue)
            {
                MaterialType item = UoW.MaterialTypeRepository.Get(model.ID.Value);
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

            return new KendoResponse<MaterialTypeAPIModel>()
            {
                Response = response
            };
        }


        public override KendoResponse<MaterialTypeAPIModel> Delete(int id)
        {
            MaterialType item = UoW.MaterialTypeRepository.Get(id);
            UoW.MaterialTypeRepository.Delete(item);

            return new KendoResponse<MaterialTypeAPIModel>()
            {
                Response = Convert(item)
            };
        }

        protected override MaterialType Bind(MaterialType item, MaterialTypeAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Identifier = model.Identifier;

            return item;
        }

        protected override MaterialTypeAPIModel Convert(MaterialType item)
        {
            if (item == null)
            {
                return null;
            }

            return new MaterialTypeAPIModel()
            {
                ID = item.ID,
                Identifier = item.Identifier
            };
        }
    }
}
