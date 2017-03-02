/**
 * @file LicenseExpiringEmailViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace Services.MailSending.Models
{
    public class LicenseExpiringEmailViewModel : EmailViewModel
    {
        public int Days { get; set; }

        public DateTime ExpirationAt { get; set; }

        public string LicenseName { get; set; }

        public string OrganizationName { get; set; }

        public string Name { get; set; }
    }
}
