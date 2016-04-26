using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(HDContext sb) : base(sb)
        {
        }
    }
}
