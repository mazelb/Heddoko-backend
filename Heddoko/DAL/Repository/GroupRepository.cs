using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace DAL
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(HDContext sb) : base(sb)
        {
        }

        public Group GetByName(string name)
        {
            return DbSet.Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public IEnumerable<Group> GetByUser(int id, List<string> embed = null)
        {
            IQueryable<Group> groups = DbSet.Where(c => c.Managers.Any(m => m.ID == id));

            if (embed != null
            && embed.Count() > 0)
            {
                foreach (string em in embed)
                {
                    switch (em)
                    {
                        case Constants.Embed.Tags:
                            groups = groups.Include(c => c.Tags);
                            break;
                        case Constants.Embed.AvatarSrc:
                            groups = groups.Include(c => c.Asset);
                            break;
                    }
                }
            }

            return groups;
        }
    }
}
