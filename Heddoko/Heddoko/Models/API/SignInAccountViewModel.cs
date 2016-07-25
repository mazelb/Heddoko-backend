using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using i18n;

namespace Heddoko.Models
{
    public class SignInAPIViewModel
    {
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
    }
}