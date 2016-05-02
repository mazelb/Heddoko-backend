using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class MaterialAPIModel : BaseAPIModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string PartNo { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public int MaterialTypeID { get; set; }

        public string MaterialTypeName { get; set; }

        public string NamePart { get; set; }
    }
}