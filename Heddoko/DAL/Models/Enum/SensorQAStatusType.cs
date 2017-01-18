/**
 * @file SensorQAStatusType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
