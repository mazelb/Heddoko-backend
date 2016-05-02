using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IEquipmentRepository : IBaseRepository<Equipment>
    {
        IEnumerable<Equipment> Search(string value);
        void RemoveComplexEquipment(int complextEquipmentID);
        IEnumerable<Equipment> GetByComplexEquipment(int complexEquipmentID);
    }
}
