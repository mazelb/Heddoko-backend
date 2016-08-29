using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class ShirtAPIModel : BaseAPIModel
    {
        public ShirtAPIModel()
        {
            QAStatus = ShirtQAStatusType.None;
        }

        public ShirtAPIModel(bool isEmpty)
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

        public ShirtQAStatusType? QAStatus { get; set; }

        public Dictionary<string, bool> QaStatuses { get; set; }

        public int? ShirtOctopiID { get; set; }

        public ShirtOctopi ShirtOctopi { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Shirt}" : $"{IDView} - {Size.GetDisplayName()}";

        public string QAStatusText => QAStatus?.ToStringFlags();

        public List<string> QAModel => QAStatus?.ToArrayStringFlags();
    }
}