/**
 * @file PowerboardQAStatusType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
