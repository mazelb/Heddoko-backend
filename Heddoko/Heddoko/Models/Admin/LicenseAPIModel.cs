using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class LicenseAPIModel : BaseAPIModel
    {
        public string ViewID { get; set; }

        public uint Amount { get; set; }

        public LicenseType Type { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [CompareToday(CompareEquality.Greater, ErrorMessageResourceName = "WrongExpirationAtDate", ErrorMessageResourceType = typeof(i18n.Resources))]
        public DateTime ExpirationAt { get; set; }

        public LicenseStatusType Status { get; set; }

        public int? Used { get; set; }

        public int? OrganizationID { get; set; }

        public string Name { get; set; }
    }
}