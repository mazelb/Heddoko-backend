﻿/**
 * @file FirmwaresController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using DAL.Models.Enum;
using Heddoko.Models;
using Heddoko.Models.Admin;
using Heddoko.Models.API;
using i18n;
using Newtonsoft.Json;
using Services;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/firmwares")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class FirmwaresController : BaseAdminController<Firmware, FirmwareAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        public FirmwaresController() { }

        public FirmwaresController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        public override KendoResponse<IEnumerable<FirmwareAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Firmware> items = null;

            bool isDeleted = false;
            bool isUsed = false;

            if (request?.Filter != null)
            {
                KendoFilterItem isUsedFilter = request.Filter.Get(Used);

                if (isUsedFilter != null)
                {
                    int tmp = 0;
                    FirmwareType type = FirmwareType.Brainpack;
                    if (int.TryParse(isUsedFilter.Value, out tmp))
                    {
                        type = (FirmwareType)tmp;
                    }

                    items = UoW.FirmwareRepository.GetByType(type);
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
                        items = UoW.FirmwareRepository.Search(searchFilter.Value, isDeleted);
                    }
                }
            }

            if (items == null
                &&
                !isUsed)
            {
                items = UoW.FirmwareRepository.All(isDeleted);
            }

            int count = items.Count();

            if (request?.Take != null
                &&
                request.Skip != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<FirmwareAPIModel> itemsDefault = new List<FirmwareAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new FirmwareAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<FirmwareAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<FirmwareAPIModel> Get(int id)
        {
            Firmware item = UoW.FirmwareRepository.Get(id);

            return new KendoResponse<FirmwareAPIModel>
            {
                Response = Convert(item)
            };
        }

        public async Task<KendoResponse<FirmwareAPIModel>> Upload()
        {
            FirmwareAPIModel model = new FirmwareAPIModel();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //TODO Moved to CustomMediaTypeFormater
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (string key in provider.FormData.AllKeys)
            {
                string[] enumerable = provider.FormData.GetValues(key);

                if (enumerable == null)
                {
                    continue;
                }

                foreach (string val in enumerable)
                {
                    switch (key)
                    {
                        case "version":
                            model.Version = val;
                            break;
                        case "type":
                            model.Type = (FirmwareType)int.Parse(val);
                            break;
                        case "status":
                            model.Status = (FirmwareStatusType)int.Parse(val);
                            break;
                        case "files":
                            model.Files = JsonConvert.DeserializeObject<List<AssetFileAPIModel>>(val);

                            foreach (AssetFileAPIModel file in model.Files)
                            {
                                if (!file.Type.IsRecordType())
                                {
                                    throw new APIException(ErrorAPIType.AssetType, $"{Resources.Wrong} type");
                                }
                            }

                            break;
                    }
                }
            }

            Firmware item = new Firmware();

            Bind(item, model);

            UoW.FirmwareRepository.Add(item);

            if (item.Type == FirmwareType.DefaultRecords)
            {
                if (provider.FileData.Count < Constants.Records.MinFilesCount || provider.FileData.Count > Constants.Records.MaxFilesCount)
                {
                    throw new Exception(string.Format(Resources.WrongFilesCount, Constants.Records.MinFilesCount, Constants.Records.MaxFilesCount));
                }

                Record record = new Record
                {
                    Type = RecordType.DefaultRecord
                };

                UoW.RecordRepository.Add(record);
                item.Record = record;

                for (int i = 0; i < provider.FileData.Count; i++)
                {
                    MultipartFileData file = provider.FileData[i];

                    string name = JsonConvert.DeserializeObject(file.Headers.ContentDisposition.FileName).ToString();

                    if (model.Files[i].FileName != name)
                    {
                        throw new Exception($"{Resources.Wrong} file data");
                    }

                    Asset asset = new Asset
                    {
                        Name = name,
                        Type = model.Files[i].Type,
                        Proccessing = AssetProccessingType.None,
                        Status = UploadStatusType.New,
                        Record = record
                    };

                    string path = AssetManager.Path($"{item.Id}/{asset.Name}", asset.Type);

                    Azure.Upload(path, DAL.Config.AssetsContainer, file.LocalFileName);
                    File.Delete(file.LocalFileName);

                    asset.Image = $"/{path}";

                    asset.Status = UploadStatusType.Uploaded;
                    UoW.AssetRepository.Add(asset);
                }
            }
            else
            {

                AssetType assetType;
                switch (item.Type)
                {
                    case FirmwareType.Guide:
                        assetType = AssetType.Guide;
                        break;
                    default:
                        assetType = AssetType.Firmware;
                        break;
                }

                Asset asset = new Asset
                {
                    Type = assetType,
                    Proccessing = AssetProccessingType.None,
                    Status = UploadStatusType.New
                };

                foreach (MultipartFileData file in provider.FileData)
                {
                    asset.Name = JsonConvert.DeserializeObject(file.Headers.ContentDisposition.FileName).ToString();
                    string path = AssetManager.Path($"{item.Id}/{asset.Name}", asset.Type);

                    Azure.Upload(path, DAL.Config.AssetsContainer, file.LocalFileName);
                    File.Delete(file.LocalFileName);

                    asset.Image = $"/{path}";
                    break;
                }
                asset.Status = UploadStatusType.Uploaded;
                UoW.AssetRepository.Add(asset);
                item.Asset = asset;
            }

            UoW.Save();

            return new KendoResponse<FirmwareAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<FirmwareAPIModel> Post(FirmwareAPIModel model)
        {
            FirmwareAPIModel response;

            if (ModelState.IsValid)
            {
                Firmware item = new Firmware();

                Bind(item, model);

                UoW.FirmwareRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<FirmwareAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<FirmwareAPIModel> Put(FirmwareAPIModel model)
        {
            FirmwareAPIModel response = new FirmwareAPIModel();

            if (model.ID.HasValue)
            {
                Firmware item = UoW.FirmwareRepository.GetFull(model.ID.Value);
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
                        throw new ModelStateException
                        {
                            ModelState = ModelState
                        };
                    }
                }
            }

            return new KendoResponse<FirmwareAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<FirmwareAPIModel> Delete(int id)
        {
            Firmware item = UoW.FirmwareRepository.Get(id);

            if (item.Id != CurrentUser.Id)
            {
                item.Status = FirmwareStatusType.Inactive;
                UoW.Save();
            }

            return new KendoResponse<FirmwareAPIModel>
            {
                Response = Convert(item)
            };
        }

        protected override Firmware Bind(Firmware item, FirmwareAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Type = model.Type;
            item.Version = model.Version?.Trim();
            item.Status = model.Status;

            return item;
        }

        protected override FirmwareAPIModel Convert(Firmware item)
        {
            if (item == null)
            {
                return null;
            }

            return new FirmwareAPIModel
            {
                ID = item.Id,
                IDView = item.IDView,
                Version = item.Version,
                Status = item.Status,
                Type = item.Type,
                Url = item.Asset?.Url,
                Files = item.Record?.Assets.Select(a => new AssetFileAPIModel { Type = a.Type, Url = a.Url, FileName = a.Name }).ToList()
            };
        }
    }
}