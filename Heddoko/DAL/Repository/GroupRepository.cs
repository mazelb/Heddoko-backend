using DAL.Models;
using System.Linq;
using System.Data.Entity;


namespace DAL
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
