using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using i18n;

namespace Heddoko.Models
{
    public class ForgotViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources))]
        [AllowHtml]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "ConfirmPasswordValidateMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources))]
        [AllowHtml]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        public string ForgetToken { get; set; }
    }
}