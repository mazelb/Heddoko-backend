using System.Web.Http;
using DAL.Models;
using Heddoko.Models;
using i18n;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using DAL;
using Services;
using System.IO;
using System.Threading.Tasks;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/firmwares")]
    public class FirmwareAPIController : BaseAPIController
    {
        public FirmwareAPIController() { }

        public FirmwareAPIController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        /// <summary>
        ///     Check firmware
        /// </summary>
        /// <param name="id">The id of brainpack or databoard or powerboard</param>
        /// <param name="type">The type of firmware.</param>
        [Route("check")]
        [HttpPost]
        public Firmware CheckFirmware(FirmwareAPIViewModel model)
        {
            if (!model.Type.HasValue)
            {
                throw new APIException(ErrorAPIType.ObjectNotFound, Resources.Type);
            }

            return UoW.FirmwareRepository.LastFirmwareByType(model.Type.Value);
        }

        /// <summary>
        ///     Update firmware
        /// </summary>
        /// <param name="id">The id of brainpack or databoard or powerboard</param>
        /// <param name="firmwareID">The id of firmware</param>
        /// <param name="type">The type of firmware.</param>
        [Route("update")]
        [HttpPost]
        public Firmware UpdateFirmware(FirmwareAPIViewModel model)
        {
            if (!model.ID.HasValue
              && string.IsNullOrEmpty(model.Label))
            {
                throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} ID or Label");
            }

            if (!model.FirmwareID.HasValue)
            {
                throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.SoftwareOrFirmware}");
            }

            Firmware firmware = UoW.FirmwareRepository.GetFull(model.FirmwareID.Value);

            switch (firmware.Type)
            {
                case FirmwareType.Brainpack:
                    Brainpack brainpack = model.ID.HasValue ? UoW.BrainpackRepository.Get(model.ID.Value) : UoW.BrainpackRepository.Get(model.Label);
                    if (brainpack == null)
                    {
                        throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.Brainpack}");
                    }

                    brainpack.Firmware = firmware;
                    break;
                case FirmwareType.Sensor:
                    Sensor sensor = model.ID.HasValue ? UoW.SensorRepository.Get(model.ID.Value) : UoW.SensorRepository.Get(model.Label);
                    if (sensor == null)
                    {
                        throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.Sensor}");
                    }

                    sensor.Firmware = firmware;
                    break;
                case FirmwareType.Powerboard:
                    Powerboard powerboard = model.ID.HasValue ? UoW.PowerboardRepository.Get(model.ID.Value) : UoW.PowerboardRepository.Get(model.Label);

                    if (powerboard == null)
                    {
                        throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.Powerboard}");
                    }

                    powerboard.Firmware = firmware;
                    break;
                case FirmwareType.Databoard:
                    Databoard databoard = model.ID.HasValue ? UoW.DataboardRepository.Get(model.ID.Value) : UoW.DataboardRepository.Get(model.Label);

                    if (databoard == null)
                    {
                        throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.Databoard}");
                    }

                    databoard.Firmware = firmware;
                    break;
            }
            UoW.Save();

            return firmware;
        }

        /// <summary>
        ///     Check software
        /// </summary>
        [Route("software")]
        [HttpGet]
        public Firmware CheckSoftware()
        {
            return UoW.FirmwareRepository.LastFirmwareByType(FirmwareType.Software);
        }

        [Route("upload/{token}/{version}")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> UploadSoftware(string token, string version)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            if (string.IsNullOrEmpty(token))
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.NotFound} token");
            }

            if (!Config.CIUploadToken.Equals(token))
            {
                throw new APIException(ErrorAPIType.WrongObjectAccess, $"{Resources.WrongObjectAccess}");
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new APIException(ErrorAPIType.Error, $"{Resources.NotFound} version");
            }

            Firmware item = new Firmware()
            {
                Type = FirmwareType.Software,
                Version = version?.Trim(),
                Status = FirmwareStatusType.Active
            };


            UoW.FirmwareRepository.Create(item);

            Asset asset = new Asset
            {
                Type = AssetType.Firmware,
                Proccessing = AssetProccessingType.None,
                Status = UploadStatusType.New
            };

            foreach (MultipartFileData file in provider.FileData)
            {
                asset.Name = file.Headers.ContentDisposition.FileName;
                string path = AssetManager.Path($"{item.Id}/{asset.Name}", asset.Type);

                Azure.Upload(path, DAL.Config.AssetsContainer, file.LocalFileName);
                File.Delete(file.LocalFileName);

                asset.Image = $"/{path}";
                break;
            }

            asset.Status = UploadStatusType.Uploaded;
            UoW.AssetRepository.Add(asset);
            item.Asset = asset;
            UoW.Save();


            return true;
        }
    }
}