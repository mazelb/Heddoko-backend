using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;
using System.Collections.Generic;

namespace Heddoko.Models
{
    public class PantsAPIModel : BaseAPIModel
    {
        public PantsAPIModel()
        {
        }

        public PantsAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Label { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Notes { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public PantsQAStatusType? QAStatus { get; set; }

        public Dictionary<string, bool> QaStatuses { get; set; }

        public int? PantsOctopiID { get; set; }

        public PantsOctopi PantsOctopi { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Pants}" : $"{IDView} - {Status.GetDisplayName()}";

        public string QAStatusText => QAStatus.ToStringFlags();

        public List<string> QAModel => QAStatus.ToArrayStringFlags();
    }
}
