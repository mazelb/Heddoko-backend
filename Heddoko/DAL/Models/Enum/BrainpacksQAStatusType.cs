using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum BrainpacksQAStatusType
    {
        FullTested = 0,
        ChargingCycleTested = 1,
        ChargingCycleFailed = 2,
        ButtonsTested = 3,
        ButtonsFailed = 4,
        LEDTested = 5,
        LEDFailed = 6,
        SensorConnectionTested = 7,
        SensorConnectionFailed = 8,
        SoftwareConnectionTested = 9,
        SoftwareConnectionFailed = 10,
        CommandTested = 11,
        CommandFailed = 12,
        StreamingTested = 13,
        StreamingFailed = 14
    }
}
