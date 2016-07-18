using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Heddoko.Models
{
    public class PantsAPIModel : BaseAPIModel
    {
        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        public int? PantsOctopiID { get; set; }

        public PantsOctopi PantsOctopi { get; set; }

        public string IDView { get; set; }
    }
}