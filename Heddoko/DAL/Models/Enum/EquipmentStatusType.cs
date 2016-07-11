using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum EquipmentStatusType
    {
        Ready = 0,
        InUse = 1,
        Defective = 2,
        InProduction = 3,
        Testing = 4
    }
}
