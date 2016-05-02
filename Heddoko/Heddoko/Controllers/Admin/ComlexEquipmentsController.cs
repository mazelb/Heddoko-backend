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
    [RoutePrefix("admin/api/complexEquipments")]
    [Auth(Roles = DAL.Constants.Roles.Admin)]
    public class ComplexEquipmentsController : BaseAdminController<ComplexEquipment, ComplexEquipmentAPIModel>
    {
        const string Search = "Search";

        public override KendoResponse<IEnumerable<ComplexEquipmentAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<ComplexEquipment> items = null;
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
                                items = UoW.ComplexEquipmentRepository.Search(searchFilter.Value);
                            }
                            break;
                    }
                }
            }

            if (items == null)
            {
                items = UoW.ComplexEquipmentRepository.All();
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

            return new KendoResponse<IEnumerable<ComplexEquipmentAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<ComplexEquipmentAPIModel> Get(int id)
        {
            ComplexEquipment item = UoW.ComplexEquipmentRepository.Get(id);

            return new KendoResponse<ComplexEquipmentAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<ComplexEquipmentAPIModel> Post(ComplexEquipmentAPIModel model)
        {
            ComplexEquipmentAPIModel response = new ComplexEquipmentAPIModel();

            if (ModelState.IsValid)
            {
                ComplexEquipment item = new ComplexEquipment();
                Bind(item, model);
                UoW.ComplexEquipmentRepository.Create(item);
                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<ComplexEquipmentAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<ComplexEquipmentAPIModel> Put(ComplexEquipmentAPIModel model)
        {
            ComplexEquipmentAPIModel response = new ComplexEquipmentAPIModel();

            if (model.ID.HasValue)
            {
                ComplexEquipment item = UoW.ComplexEquipmentRepository.GetFull(model.ID.Value);
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {
                        Bind(item, model);
                        UoW.ComplexEquipmentRepository.Update();

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

            return new KendoResponse<ComplexEquipmentAPIModel>()
            {
                Response = response
            };
        }


        public override KendoResponse<ComplexEquipmentAPIModel> Delete(int id)
        {
            ComplexEquipment item = UoW.ComplexEquipmentRepository.Get(id);
            UoW.EquipmentRepository.RemoveComplexEquipment(item.ID);
            UoW.ComplexEquipmentRepository.Delete(item);

            return new KendoResponse<ComplexEquipmentAPIModel>()
            {
                Response = Convert(item)
            };
        }

        protected override ComplexEquipment Bind(ComplexEquipment item, ComplexEquipmentAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.MacAddress = model.MacAddress;
            item.SerialNo = model.SerialNo;
            item.Notes = model.Notes;
            item.PhysicalLocation = model.PhysicalLocation;
            item.Status = model.Status;

            if (item.Equipments != null)
            {
                foreach (Equipment equipment in item.Equipments)
                {
                    if (!model.Equipments.Any(c => c.ID == equipment.ID))
                    {
                        item.Equipments.Remove(equipment);
                    }
                }
            }

            if (model.Equipments != null)
            {
                foreach (Equipment equipment in model.Equipments)
                {
                    if (item.Equipments != null
                    && !item.Equipments.Any(c => c.ID == equipment.ID))
                    {
                        Equipment equip = UoW.EquipmentRepository.Get(equipment.ID);
                        if (equip != null)
                        {
                            item.Equipments.Add(equip);
                        }
                    }
                }
            }
            return item;
        }

        protected override ComplexEquipmentAPIModel Convert(ComplexEquipment item)
        {
            if (item == null)
            {
                return null;
            }

            return new ComplexEquipmentAPIModel()
            {
                ID = item.ID,
                MacAddress = item.MacAddress,
                SerialNo = item.SerialNo,
                Notes = item.Notes,
                PhysicalLocation = item.PhysicalLocation,
                Status = item.Status
            };
        }
    }
}
