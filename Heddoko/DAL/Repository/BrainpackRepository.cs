using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;

namespace DAL
{
    public class BrainpackRepository : BaseRepository<Brainpack>, IBrainpackRepository
    {
        public BrainpackRepository(HDContext sb)
            : base(sb)
        {
        }

        public void RemoveDataboard(int databoardID)
        {
            DbSet.Where(c => c.DataboardID.Value == databoardID).Update(c => new Brainpack()
            {
                DataboardID = null
            });
        }
    }
}