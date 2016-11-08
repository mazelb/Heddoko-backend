using DAL.Models;

namespace DAL.Repository.Interface
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        Device GetByToken(string token);

        void DeleteByToken(string token);
    }
}
