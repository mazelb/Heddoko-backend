using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IComplexEquipmentRepository : IBaseRepository<ComplexEquipment>
    {
        IEnumerable<ComplexEquipment> Search(string value);
        ComplexEquipment GetFull(int value);
    }
}
