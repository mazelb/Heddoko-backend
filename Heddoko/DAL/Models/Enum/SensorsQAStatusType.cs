using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum SensorsQAStatusType
    {
        FullTested = 0,
        FirmavareTested = 1,
        PowerTested = 2,
        OrientationTested = 3,
        FirmavareFailed = 4,
        PowerFailed = 5,
        OrientationFailed = 6
    }
}
