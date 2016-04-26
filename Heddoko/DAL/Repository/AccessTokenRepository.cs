using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class AccessTokenRepository : BaseRepository<AccessToken>, IAccessTokenRepository
    {
        public AccessTokenRepository(HDContext sb) : base(sb)
        {
        }

        public AccessToken GetByToken(string token)
        {
            return DbSet.Include(c => c.User)
                        .Where(c => c.Token == token).FirstOrDefault();
        }
    }
}
