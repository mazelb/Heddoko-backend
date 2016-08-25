using System;

namespace DAL.Models
{
    [Flags]
    public enum PowerboardQAStatusType : long
    {
        None = 0,
        PowerboardProgrammed = 1,
        PowerboardUSBEnum = 2,
        BatteryInstalled = 4,
        TestedAndReady = PowerboardProgrammed | PowerboardUSBEnum | BatteryInstalled
    }
}
