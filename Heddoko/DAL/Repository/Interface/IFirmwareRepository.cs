/**
 * @file IFirmwareRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IFirmwareRepository : IBaseRepository<Firmware>
    {
        IEnumerable<Firmware> All(bool isDeleted);
        IEnumerable<Firmware> Search(string value, bool isDeleted);
        IEnumerable<Firmware> GetByType(FirmwareType type, int? take = null, int? skip = null);
        Firmware LastFirmwareByType(FirmwareType software);
        int GetCountByType(FirmwareType type);
    }
}
