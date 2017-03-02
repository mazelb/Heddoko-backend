/**
 * @file SignUpAccountViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class SignUpAccountViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "AccountType", ResourceType = typeof(Resources))]
        public UserRoleType Role { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Firstname", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Lastname", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "ValidateEmailMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Username", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        [AllowHtml]
        public string Password { get; set; }

        [MaxLength(20)]
        [Display(Name = "Country", ResourceType = typeof(Resources))]
        public string Country { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(Resources))]
        public DateTime? Birthday { get; set; }

        [MaxLength(50)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceName = "InvalidPhone", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Mobile", ResourceType = typeof(Resources))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "OrganizationName", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        public string OrganizationName { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Address", ResourceType = typeof(Resources))]
        [MaxLength(255)]
        public string Address { get; set; }

        public string InviteToken { get; set; }

        public Organization Organization { get; set; }

        public int UserId { get; set; }
    }
}