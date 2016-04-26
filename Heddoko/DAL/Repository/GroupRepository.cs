using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(HDContext sb) : base(sb)
        {
        }

        public Group GetByName(string name)
        {
            return DbSet.Where(c => c.Name == name).FirstOrDefault();
        }
    }
}
