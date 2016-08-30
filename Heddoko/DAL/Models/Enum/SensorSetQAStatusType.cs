using System;

namespace DAL.Models
{
    [Flags]
    public enum SensorSetQAStatusType : long
    {
        None = 0,
        TestedAndReady = 1
    }
}
