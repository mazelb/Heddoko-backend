using DAL.Models;

namespace DAL
{
    public interface IKitRepository : IBaseRepository<Kit>
    {
        void RemoveBrainpack(int iD);
    }
}