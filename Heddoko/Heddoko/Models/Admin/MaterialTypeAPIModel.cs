using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class MaterialTypeAPIModel : BaseAPIModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(50, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Identifier { get; set; }
    }
}