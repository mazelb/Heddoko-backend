using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IFirmwareRepository : IBaseRepository<Firmware>
    {
        IEnumerable<Firmware> All(bool isDeleted);
        IEnumerable<Firmware> Search(string value, bool isDeleted);
        IEnumerable<Firmware> GetByType(FirmwareType type);
    }
}
