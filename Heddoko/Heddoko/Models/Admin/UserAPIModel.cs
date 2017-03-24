/**
 * @file UserAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
        public string Email { get; set; }

        public string Username { get; set; }

        [MaxLength(50)]
        public string Firstname { get; set; }

        [MaxLength(50)]
        public string Lastname { get; set; }

        public UserRoleType Role { get; set; }

        public UserStatusType? Status { get; set; }

        public DateTime? ExpirationAt { get; set; }

        [MaxLength(50)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceName = "InvalidPhone", ErrorMessageResourceType = typeof(Resources))]
        public string Phone { get; set; }

        public int? KitID { get; set; }

        public int? TeamID { get; set; }

        public Team Team { get; set; }
    }
}