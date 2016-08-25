using System;

namespace DAL.Models
{
    [Flags]
    public enum SensorQAStatusType : long
    {
        None = 0,
        FirmwareUpdated = 1,
        SeatingInBase = 2,
        LED = 4,
        Orientation = 8,
        Drift = 26,
        TestedAndReady = FirmwareUpdated | SeatingInBase | LED | Orientation | Drift
    }
}
