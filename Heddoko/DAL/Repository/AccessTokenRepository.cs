using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class AccessTokenRepository : BaseRepository<AccessToken>, IAccessTokenRepository
    {
        public AccessTokenRepository(HDContext sb)
            : base(sb)
        {
        }

        public AccessToken GetByToken(string token)
        {
            return DbSet.Include(c => c.User)
                        .FirstOrDefault(c => c.Token == token);
        }
    }
}