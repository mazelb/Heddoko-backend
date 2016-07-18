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
        FirmavareFailed = 2,
        PowerTested = 3,
        PowerFailed = 4,
        OrientationTested = 5,
        OrientationFailed = 6,
        Failed = 7
    }
}
