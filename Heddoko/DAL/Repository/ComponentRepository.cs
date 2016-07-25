using DAL.Models;

namespace DAL
{
    public class ComponentRepository : BaseRepository<Component>, IComponentRepository
    {
        public ComponentRepository(HDContext sb)
            : base(sb)
        {
        }
    }
}