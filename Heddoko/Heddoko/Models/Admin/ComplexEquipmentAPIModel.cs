using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ComplexEquipmentAPIModel : BaseAPIModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string MacAddress { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string SerialNo { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string PhysicalLocation { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Notes { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public EquipmentStatusType Status { get; set; }

        public string Equipments { get; set; }
    }
}