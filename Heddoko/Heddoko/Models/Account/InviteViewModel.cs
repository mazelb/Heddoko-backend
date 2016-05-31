using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Models
{
    public class InviteViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "InviteToken", ResourceType = typeof(i18n.Resources))]
        [AllowHtml]
        public string InviteToken { get; set; }

        public string Message { get; set; }
    }
}