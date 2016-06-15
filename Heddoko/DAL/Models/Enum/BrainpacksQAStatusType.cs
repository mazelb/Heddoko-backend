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
        ButtonsTested = 2,
        LEDTested = 3,
        SensorConnectionTested = 4,
        SoftwareConnectionTested = 5,
        CommandTested = 6,
        StreamingTested = 7
    }
}
