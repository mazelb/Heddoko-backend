using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IEquipmentRepository : IBaseRepository<Equipment>
    {
        IEnumerable<Equipment> Search(string value);
    }
}
