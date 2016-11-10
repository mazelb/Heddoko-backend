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
