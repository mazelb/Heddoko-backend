using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class ComponentRepository : BaseRepository<Component>, IComponentRepository
    {
        public ComponentRepository(HDContext sb) : base(sb)
        {
        }
    }
}
