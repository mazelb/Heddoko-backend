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
    [RoutePrefix("admin/api/materials")]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public class MaterialsController : BaseAdminController<Material, MaterialAPIModel>
    {
        const string Search = "Search";

        public override KendoResponse<IEnumerable<MaterialAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<Material> items = null;
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
                                items = UoW.MaterialRepository.Search(searchFilter.Value);
                            }
                            break;
                    }
                }
            }

            if (items == null)
            {
                items = UoW.MaterialRepository.All();
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

            return new KendoResponse<IEnumerable<MaterialAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<MaterialAPIModel> Get(int id)
        {
            Material item = UoW.MaterialRepository.Get(id);

            return new KendoResponse<MaterialAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<MaterialAPIModel> Post(MaterialAPIModel model)
        {
            MaterialAPIModel response = new MaterialAPIModel();

            if (ModelState.IsValid)
            {
                Material item = new Material();
                Bind(item, model);
                UoW.MaterialRepository.Create(item);
                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<MaterialAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<MaterialAPIModel> Put(MaterialAPIModel model)
        {
            MaterialAPIModel response = new MaterialAPIModel();

            if (model.ID.HasValue)
            {
                Material item = UoW.MaterialRepository.Get(model.ID.Value);
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

            return new KendoResponse<MaterialAPIModel>()
            {
                Response = response
            };
        }


        public override KendoResponse<MaterialAPIModel> Delete(int id)
        {
            Material item = UoW.MaterialRepository.Get(id);
            UoW.MaterialRepository.Delete(item);

            return new KendoResponse<MaterialAPIModel>()
            {
                Response = Convert(item)
            };
        }

        protected override Material Bind(Material item, MaterialAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Name = model.Name;
            item.PartNo = model.PartNo;

            MaterialType type = UoW.MaterialTypeRepository.Get(model.MaterialTypeID);
            if(type != null)
            {
                item.MaterialType = type;
            }

            return item;
        }

        protected override MaterialAPIModel Convert(Material item)
        {
            if (item == null)
            {
                return null;
            }

            return new MaterialAPIModel()
            {
                ID = item.ID,
                Name = item.Name,
                PartNo = item.PartNo,
                MaterialTypeID = item.MaterialTypeID,
                MaterialTypeName = item.MaterialType.Identifier,
                NamePart = $"{item.Name} {item.PartNo}"
            };
        }
    }
}
