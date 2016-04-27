using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Models
{

    public class ForgotViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "NewPassword", ResourceType = typeof(i18n.Resources))]
        [AllowHtml]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "ConfirmPasswordValidateMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(i18n.Resources))]
        [AllowHtml]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string ForgetToken { get; set; }
    }
}