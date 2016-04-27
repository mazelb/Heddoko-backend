using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Models
{
    public class ConfirmViewModel : BaseViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "ConfirmToken", ResourceType = typeof(i18n.Resources))]
        [AllowHtml]
        public string ConfirmToken { get; set; }

        public string Message { get; set; }
    }
}