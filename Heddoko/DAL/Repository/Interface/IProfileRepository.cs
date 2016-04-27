using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        IEnumerable<Profile> GetByUser(int id, List<string> embed = null);
    }
}
