using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Group GetByName(string name);
        IEnumerable<Group> GetByUser(int id, List<string> embed = null);
    }
}
