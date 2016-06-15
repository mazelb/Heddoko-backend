﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Models
{
    public class ProfileAccountViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "Firstname", ResourceType = typeof(i18n.Resources))]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "Lastname", ResourceType = typeof(i18n.Resources))]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "ValidateEmailMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "Email", ResourceType = typeof(i18n.Resources))]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "Username", ResourceType = typeof(i18n.Resources))]
        [MaxLength(50)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "OldPassword", ResourceType = typeof(i18n.Resources))]
        [AllowHtml]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Password", ResourceType = typeof(i18n.Resources))]
        [AllowHtml]
        public string NewPassord { get; set; }

        [MaxLength(20)]
        [Display(Name = "Country", ResourceType = typeof(i18n.Resources))]
        public string Country { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(i18n.Resources))]
        public DateTime? Birthday { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "Mobile", ResourceType = typeof(i18n.Resources))]
        public string Phone { get; set; }

        [Display(Name = "OrganizationName", ResourceType = typeof(i18n.Resources))]
        [MaxLength(50)]
        public string OrganizationName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(i18n.Resources))]
        [MaxLength(255)]
        public string Address { get; set; }

        public Organization Organization { get; set; }
    }
}