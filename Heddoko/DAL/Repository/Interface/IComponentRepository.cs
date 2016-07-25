using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IComponentRepository : IBaseRepository<Component>
    {
        IEnumerable<Component> Search(string value, bool isDeleted);
        IEnumerable<Component> All(bool isDeleted);
    }
}