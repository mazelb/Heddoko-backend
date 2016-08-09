using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using i18n;

namespace Heddoko.Models
{
    public class ConfirmViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ConfirmToken", ResourceType = typeof(Resources))]
        [AllowHtml]
        public string ConfirmToken { get; set; }

        public string Message { get; set; }
    }
}