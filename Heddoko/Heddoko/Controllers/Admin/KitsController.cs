﻿/**
 * @file KitsController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;
using Microsoft.AspNet.Identity;
using Constants = DAL.Constants;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/kits")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndAdmin)]
    public class KitsController : BaseAdminController<Kit, KitAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Status = "Status";
        private const string Used = "Used";
        private const int NoBrainpackID = 0;
        private const int NoShirtID = 0;
        private const int NoPantsID = 0;
        private const int NoSensorSetID = 0;

        public KitsController() { }

        public KitsController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        public override KendoResponse<IEnumerable<KitAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Kit> items = null;

            int? organizationID = null;
            if (UserManager.IsInRole(CurrentUser.Id, Constants.Roles.LicenseAdmin))
            {
                if (!CurrentUser.OrganizationID.HasValue)
                {
                    throw new Exception(Resources.WrongObjectAccess);
                }

                organizationID = CurrentUser.OrganizationID;
            }

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
                    items = UoW.KitRepository.GetAvailable(usedID, organizationID);
                    isUsed = true;
                }
                else
                {
                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }

                    //Setup Search
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
                        items = UoW.KitRepository.Search(searchFilter?.Value, statusInt, isDeleted, organizationID);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.KitRepository.All(isDeleted, organizationID);
            }

            int count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<KitAPIModel> itemsDefault = new List<KitAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new KitAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<KitAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<KitAPIModel> Get(int id)
        {
            Kit item = UoW.KitRepository.Get(id);

            return new KendoResponse<KitAPIModel>
            {
                Response = Convert(item)
            };
        }

        [AuthAPI(Roles = Constants.Roles.Admin)]
        public override KendoResponse<KitAPIModel> Post(KitAPIModel model)
        {
            KitAPIModel response;

            if (ModelState.IsValid)
            {
                Kit item = new Kit();

                Bind(item, model);

                UoW.KitRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<KitAPIModel>
            {
                Response = response
            };
        }

        [AuthAPI(Roles = Constants.Roles.Admin)]
        public override KendoResponse<KitAPIModel> Put(KitAPIModel model)
        {
            KitAPIModel response = new KitAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<KitAPIModel>
                {
                    Response = response
                };
            }


            Kit item = UoW.KitRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<KitAPIModel>
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

            return new KendoResponse<KitAPIModel>
            {
                Response = response
            };
        }

        [AuthAPI(Roles = Constants.Roles.Admin)]
        public override KendoResponse<KitAPIModel> Delete(int id)
        {
            Kit item = UoW.KitRepository.Get(id);

            if (item.Id != CurrentUser.Id)
            {
                item.Status = EquipmentStatusType.Trash;
                item.OrganizationID = null;
                item.UserID = null;
                item.SensorSetID = null;
                item.BrainpackID = null;
                item.ShirtID = null;
                item.PantsID = null;
                UoW.Save();
            }

            return new KendoResponse<KitAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            List<HistoryNotes> item = UoW.KitRepository.HistoryNotes(id);

            return new KendoResponse<IEnumerable<HistoryNotes>>
            {
                Response = item
            };
        }

        protected override Kit Bind(Kit item, KitAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            if (!IsAdmin)
            {
                throw new Exception(Resources.WrongObjectAccess);
            }

            if (IsLicenseAdmin)
            {
                //TODO remove that
                if (model.UserID.HasValue)
                {
                    item.User = UoW.UserRepository.Get(model.UserID.Value);
                }
            }
            else
            {
                if (model.BrainpackID.HasValue
                        &&
                        (!item.BrainpackID.HasValue || (model.BrainpackID.Value != item.BrainpackID.Value)))
                {
                    if (model.BrainpackID.Value == NoBrainpackID)
                    {
                        if (item.Brainpack != null
                            &&
                            item.Brainpack.Status == EquipmentStatusType.InUse)
                        {
                            item.Brainpack.Status = EquipmentStatusType.Ready;
                        }
                        item.Brainpack = null;
                    }
                    else
                    {
                        Brainpack brainpack = UoW.BrainpackRepository.GetFull(model.BrainpackID.Value);

                        if (brainpack.Kit != null)
                        {
                            throw new Exception($"{Resources.Brainpack} {Resources.AlreadyUsed}");
                        }

                        item.Brainpack = brainpack;
                        brainpack.Status = EquipmentStatusType.InUse;
                    }
                }

                if (model.SensorSetID.HasValue
                    &&
                    (!item.SensorSetID.HasValue || (model.SensorSetID.Value != item.SensorSetID.Value)))
                {
                    if (model.SensorSetID.Value == NoSensorSetID)
                    {
                        if (item.SensorSet != null
                            &&
                            item.SensorSet.Status == EquipmentStatusType.InUse)
                        {
                            item.SensorSet.Status = EquipmentStatusType.Ready;
                        }
                        item.SensorSet = null;
                    }
                    else
                    {
                        SensorSet sensorSet = UoW.SensorSetRepository.GetFull(model.SensorSetID.Value);

                        if (sensorSet.Kit != null)
                        {
                            throw new Exception($"{Resources.SensorSet} {Resources.AlreadyUsed}");
                        }

                        item.SensorSet = sensorSet;
                        sensorSet.Status = EquipmentStatusType.InUse;
                    }
                }

                if (model.PantsID.HasValue
                    &&
                    (!item.PantsID.HasValue || (model.PantsID.Value != item.PantsID.Value)))
                {
                    if (model.PantsID.Value == NoPantsID)
                    {
                        if (item.Pants != null
                            &&
                            item.Pants.Status == EquipmentStatusType.InUse)
                        {
                            item.Pants.Status = EquipmentStatusType.Ready;
                        }
                        item.Pants = null;
                    }
                    else
                    {
                        Pants pants = UoW.PantsRepository.GetFull(model.PantsID.Value);

                        if (pants.Kit != null)
                        {
                            throw new Exception($"{Resources.Pants} {Resources.AlreadyUsed}");
                        }

                        item.Pants = pants;
                        pants.Status = EquipmentStatusType.InUse;
                    }
                }

                if (model.ShirtID.HasValue
                    &&
                    (!item.ShirtID.HasValue || (model.ShirtID.Value != item.ShirtID.Value)))
                {
                    if (model.ShirtID.Value == NoShirtID)
                    {
                        if (item.Shirt != null
                            &&
                            item.Shirt.Status == EquipmentStatusType.InUse)
                        {
                            item.Shirt.Status = EquipmentStatusType.Ready;
                        }
                        item.Shirt = null;
                    }
                    else
                    {
                        Shirt shirt = UoW.ShirtRepository.GetFull(model.ShirtID.Value);

                        if (shirt.Kit.Any())
                        {
                            throw new Exception($"{Resources.Shirts} {Resources.AlreadyUsed}");
                        }

                        item.Shirt = shirt;
                        shirt.Status = EquipmentStatusType.InUse;
                    }
                }

                if (model.OrganizationID.HasValue)
                {
                    item.Organization = UoW.OrganizationRepository.Get(model.OrganizationID.Value);
                }

                item.Status = model.Status;
                item.Location = model.Location?.Trim(); ;
                item.QAStatus = model.QAStatus;
                item.Notes = model.Notes?.Trim();
                item.Label = model.Label?.Trim();
                item.Composition = model.Composition;
            }

            return item;
        }

        protected override KitAPIModel Convert(Kit item)
        {
            if (item == null)
            {
                return null;
            }

            return new KitAPIModel
            {
                ID = item.Id,
                IDView = item.IDView,
                Location = item.Location,
                Status = item.Status,
                Composition = item.Composition,
                SensorSet = item.SensorSet,
                Brainpack = item.Brainpack,
                SensorSetID = item.SensorSetID ?? 0,
                Organization = item.Organization,
                ShirtID = item.ShirtID ?? 0,
                Pants = item.Pants,
                PantsID = item.PantsID ?? 0,
                OrganizationID = item.OrganizationID ?? 0,
                User = item.User,
                BrainpackID = item.BrainpackID ?? 0,
                Shirt = item.Shirt,
                UserID = item.ShirtID ?? 0,
                Label = item.Label,
                Notes = item.Notes,
                QAStatus = item.QAStatus
            };
        }
    }
}