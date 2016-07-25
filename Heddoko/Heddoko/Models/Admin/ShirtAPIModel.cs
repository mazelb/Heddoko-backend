using System.ComponentModel.DataAnnotations;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class ShirtAPIModel : BaseAPIModel
    {
        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        public int? ShirtOctopiID { get; set; }

        public ShirtOctopi ShirtOctopi { get; set; }

        public string IDView { get; set; }
    }
}