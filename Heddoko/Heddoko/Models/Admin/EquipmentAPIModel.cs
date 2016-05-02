using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class EquipmentAPIModel : BaseAPIModel
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

        public AnatomicalPositionType? AnatomicalPosition { get; set; }

        public int? ComplexEquipmentID { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public PrototypeType Prototype { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public ConditionType Condition { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public NumbersType Numbers { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public HeatsShrinkType HeatsShrink { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public ShipType Ship { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public int MaterialID { get; set; }

        public string MaterialName { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public int? VerifiedByID { get; set; }

        public string VerifiedByName { get; set; }
    }
}