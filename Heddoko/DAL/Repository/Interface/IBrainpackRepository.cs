using DAL.Models;

namespace DAL
{
    public interface IBrainpackRepository : IBaseRepository<Brainpack>
    {
        void RemoveDataboard(int databoardID);
    }
}