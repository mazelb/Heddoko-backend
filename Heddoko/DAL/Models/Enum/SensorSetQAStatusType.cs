/**
 * @file SensorSetQAStatusType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
