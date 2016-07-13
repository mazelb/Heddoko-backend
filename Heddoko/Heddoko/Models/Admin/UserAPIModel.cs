using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class UserAPIModel : BaseAPIModel
    {
        public string OrganizationName { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "ValidateEmailMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Email { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public UserRoleType? Role { get; set; }

        public UserStatusType? Status { get; set; }

        public int? LicenseID { get; set; }

        public string LicenseName { get; set; }

        public LicenseStatusType? LicenseStatus { get; set; }

        public DateTime? ExpirationAt { get; set; }

        public string Phone { get; set; }
    }
}