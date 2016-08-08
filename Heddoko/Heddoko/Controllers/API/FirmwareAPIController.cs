﻿using System.Web.Http;
using DAL.Models;
using Heddoko.Models;
using i18n;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/firmwares")]
    public class FirmwareAPIController : BaseAPIController
    {
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
            if (!model.ID.HasValue)
            {
                throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} ID");
            }

            if (!model.FirmwareID.HasValue)
            {
                throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.SoftwareOrFirmware}");
            }

            Firmware firmware = UoW.FirmwareRepository.GetFull(model.FirmwareID.Value);

            switch (firmware.Type)
            {
                case FirmwareType.Brainpack:
                    Brainpack brainpack = UoW.BrainpackRepository.Get(model.ID.Value);
                    if (brainpack == null)
                    {
                        throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.Brainpack}");
                    }

                    brainpack.Firmware = firmware;
                    break;
                case FirmwareType.Powerboard:
                    Powerboard powerboard = UoW.PowerboardRepository.Get(model.ID.Value);

                    if (powerboard == null)
                    {
                        throw new APIException(ErrorAPIType.ObjectNotFound, $"{Resources.NotFound} {Resources.Powerboard}");
                    }

                    powerboard.Firmware = firmware;
                    break;
                case FirmwareType.Databoard:
                    Databoard databoard = UoW.DataboardRepository.Get(model.ID.Value);

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
    }
}