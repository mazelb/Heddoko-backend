using System.ComponentModel.DataAnnotations;
using DAL.Models.Enum;

namespace Heddoko.Models.API
{
    public class SubscribeTokenAPIViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [MaxLength(255)]
        public string Token { get; set; }

        public DeviceType Type { get; set; }
    }
}