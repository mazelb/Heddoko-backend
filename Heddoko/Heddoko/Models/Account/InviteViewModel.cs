using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using i18n;

namespace Heddoko.Models
{
    public class InviteViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "InviteToken", ResourceType = typeof(Resources))]
        [AllowHtml]
        public string InviteToken { get; set; }

        public string Message { get; set; }
    }
}