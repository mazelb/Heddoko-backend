/**
 * @file LicenseInfo.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace DAL.Models
{
    public class LicenseInfo
    {
        public LicenseStatusType Status { get; set; }

        public LicenseType Type { get; set; }

        public DateTime ExpiredAt { get; set; }

        public string Name { get; set; }

        public string ViewID { get; set; }

        public string IDView { get; set; }

        public int ID { get; set; }
    }
}