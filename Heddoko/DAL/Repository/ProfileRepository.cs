using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DAL
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(HDContext sb) : base(sb)
        {
        }

        public IEnumerable<Profile> GetByUser(int id, List<string> embed = null)
        {
            IQueryable<Profile> profiles = DbSet.Include(c => c.Tag)
                                                .Include(c => c.Tags);

            if (embed != null
             && embed.Count() > 0)
            {
                foreach (string em in embed)
                {
                    switch (em)
                    {
                        case Constants.Embed.Groups:
                            profiles = profiles.Include(c => c.Groups);
                            break;
                        case Constants.Embed.AvatarSrc:
                            profiles = profiles.Include(c => c.Asset);
                            break;
                    }
                }
            }

            return profiles.Where(c => c.Managers.Any(m => m.ID == id));
        }
    }
}
