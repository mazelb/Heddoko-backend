/**
 * @file LicenseAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.ComponentModel.DataAnnotations;
using DAL.Models;
using Heddoko.Helpers.MVC;
using i18n;

namespace Heddoko.Models
{
    public class LicenseAPIModel : BaseAPIModel
    {
        public string ViewID { get; set; }

        public uint Amount { get; set; }

        [DisallowEnumValue(typeof(LicenseType), LicenseType.Universal, ErrorMessageResourceName = "IsInvalidMessage", ErrorMessageResourceType = typeof(Resources))]
        public LicenseType Type { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [CompareToday(CompareEquality.Greater, ErrorMessageResourceName = "WrongExpirationAtDate", ErrorMessageResourceType = typeof(Resources))]
        public DateTime ExpirationAt { get; set; }

        public LicenseStatusType Status { get; set; }

        public int? Used { get; set; }

        public string IDView { get; set; }

        public int? OrganizationID { get; set; }

        public string Name { get; set; }
    }
}