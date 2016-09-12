using System;
using System.ComponentModel.DataAnnotations;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class UserAPIModel : BaseAPIModel
    {
        public string OrganizationName { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "ValidateEmailMessage", ErrorMessageResourceType = typeof(Resources))]
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

        public int? KitID { get; set; }

        public Kit Kit { get; set; }

        public int? TeamID { get; set; }

        public Team Team { get; set; }
    }
}