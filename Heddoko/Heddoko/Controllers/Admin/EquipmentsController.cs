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
    [RoutePrefix("admin/api/equipments")]
    [Auth(Roles = DAL.Constants.Roles.Admin)]
    public class EquipmentsController : BaseAdminController<Equipment, EquipmentAPIModel>
    {
        const string Search = "Search";

        public override KendoResponse<IEnumerable<EquipmentAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<Equipment> items = null;
            int count = 0;
            if (request != null)
            {
                if (request.Filter != null)
                {
                    switch (request.Filter.Filters.Count())
                    {

                        case 1:
                            KendoFilterItem searchFilter = request.Filter.Get(Search);
                            if (searchFilter != null
                            && !string.IsNullOrEmpty(searchFilter.Value))
                            {
                                items = UoW.EquipmentRepository.Search(searchFilter.Value);
                            }
                            break;
                    }
                }
            }

            if (items == null)
            {
                items = UoW.EquipmentRepository.All();
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

            return new KendoResponse<IEnumerable<EquipmentAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<EquipmentAPIModel> Get(int id)
        {
            Equipment item = UoW.EquipmentRepository.Get(id);

            return new KendoResponse<EquipmentAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<EquipmentAPIModel> Post(EquipmentAPIModel model)
        {
            EquipmentAPIModel response = new EquipmentAPIModel();

            if (ModelState.IsValid)
            {
                Equipment item = new Equipment();
                Bind(item, model);
                UoW.EquipmentRepository.Create(item);
                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<EquipmentAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<EquipmentAPIModel> Put(EquipmentAPIModel model)
        {
            EquipmentAPIModel response = new EquipmentAPIModel();

            if (model.ID.HasValue)
            {
                Equipment item = UoW.EquipmentRepository.Get(model.ID.Value);
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {
                        Bind(item, model);
                        UoW.EquipmentRepository.Update();

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

            return new KendoResponse<EquipmentAPIModel>()
            {
                Response = response
            };
        }


        public override KendoResponse<EquipmentAPIModel> Delete(int id)
        {
            Equipment item = UoW.EquipmentRepository.Get(id);
            UoW.EquipmentRepository.Delete(item);

            return new KendoResponse<EquipmentAPIModel>()
            {
                Response = Convert(item)
            };
        }

        protected override Equipment Bind(Equipment item, EquipmentAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.MacAddress = model.MacAddress;
            item.SerialNo = model.SerialNo;
            item.AnatomicalPosition = model.AnatomicalPosition;
            item.Condition = model.Condition;
            item.HeatsShrink = model.HeatsShrink;
            item.Notes = model.Notes;
            item.Numbers = model.Numbers;
            item.PhysicalLocation = model.PhysicalLocation;
            item.Prototype = model.Prototype;
            item.Ship = model.Ship;
            item.Status = model.Status;

            Material material = UoW.MaterialRepository.Get(model.MaterialID);
            if (material != null)
            {
                item.Material = material;
            }

            if (model.VerifiedByID.HasValue)
            {
                User user = UoW.UserRepository.Get(model.VerifiedByID.Value);
                if (material != null)
                {
                    item.VerifiedBy = user;
                }
            }

            return item;
        }

        protected override EquipmentAPIModel Convert(Equipment item)
        {
            if (item == null)
            {
                return null;
            }

            return new EquipmentAPIModel()
            {
                ID = item.ID,
                MacAddress = item.MacAddress,
                SerialNo = item.SerialNo,
                AnatomicalPosition = item.AnatomicalPosition,
                Condition = item.Condition,
                HeatsShrink = item.HeatsShrink,
                Notes = item.Notes,
                Numbers = item.Numbers,
                PhysicalLocation = item.PhysicalLocation,
                Prototype = item.Prototype,
                Ship = item.Ship,
                Status = item.Status,
                MaterialID = item.MaterialID,
                MaterialName = item.Material.Name,
                VerifiedByID = item.VerifiedByID,
                VerifiedByName = item.VerifiedBy.Name
            };
        }
    }
}
