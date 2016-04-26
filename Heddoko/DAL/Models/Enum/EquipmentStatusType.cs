using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum EquipmentStatusType
    {
        Unavailable = 0,
        Available = 1,
        OnLoan = 2,
        InTransit = 3
    }
}
