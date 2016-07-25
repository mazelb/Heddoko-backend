using System;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;

namespace DAL
{
    public class KitRepository : BaseRepository<Kit>, IKitRepository
    {
        public KitRepository(HDContext sb)
            : base(sb)
        {
        }


        public void RemoveBrainpack(int brainpackID)
        {
            DbSet.Where(c => c.BrainpackID.Value == brainpackID).Update(c => new Kit()
            {
                BrainpackID = null
            });
        }
    }
}