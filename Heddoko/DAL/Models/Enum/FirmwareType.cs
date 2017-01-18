﻿/**
 * @file FirmwareType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Models
{
    public enum FirmwareType
    {
        Brainpack = 0,
        Powerboard = 1,
        Databoard = 2,
        Software = 3,
        Sensor = 4,
        Guide = 5,
        DefaultRecords = 6
    }
}
