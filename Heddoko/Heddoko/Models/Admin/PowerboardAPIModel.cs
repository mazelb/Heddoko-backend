using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DAL;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class PowerboardAPIModel : BaseAPIModel
    {
        public PowerboardAPIModel()
        {
        }

        public PowerboardAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Version { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Label { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Notes { get; set; }

        public EquipmentStatusType Status { get; set; }

        public PowerboardQAStatusType? QAStatus { get; set; }

        public Dictionary<string, bool> QaStatuses { get; set; }

        public int? FirmwareID { get; set; }

        public Firmware Firmware { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Powerboard}" : $"{IDView} - {Status.GetDisplayName()}";

        public string QAStatusText => QAStatus?.ToStringFlags();

        public List<string> QAModel => QAStatus?.ToArrayStringFlags();
    }
}