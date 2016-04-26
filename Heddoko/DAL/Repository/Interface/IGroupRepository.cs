using DAL.Models;

namespace DAL
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Group GetByName(string name);
    }
}
