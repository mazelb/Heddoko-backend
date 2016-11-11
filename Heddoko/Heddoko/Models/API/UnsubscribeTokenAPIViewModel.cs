using System.ComponentModel.DataAnnotations;
using i18n;

namespace Heddoko.Models.API
{
    public class UnsubscribeTokenAPIViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(255)]
        public string Token { get; set; }
    }
}