using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;
using System.Collections.Generic;

namespace Heddoko.Models
{
    public class SensorAPIModel : BaseAPIModel
    {
        public SensorAPIModel()
        {
        }

        public SensorAPIModel(bool isEmpty)
        {
            this.IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; set; }

        public string IDView { get; set; }

        public SensorType Type { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Version { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Label { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Notes { get; set; }

        public int? FirmwareID { get; set; }

        public Firmware Firmware { get; set; }

        public EquipmentStatusType Status { get; set; }
        
        public SensorQAStatusType? QAStatus { get; set; }

        public Dictionary<string, bool> QaStatuses { get; set; }

        public int? SensorSetID { get; set; }

        public SensorSet SensorSet { get; set; }

        public AnatomicalLocationType AnatomicalLocation { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Sensors}" : $"{IDView} - {Type.GetDisplayName()} - {QAStatus.GetDisplayName()}";

        public string QAStatusText => QAStatus.ToStringFlags();

        public List<string> QAModel => QAStatus.ToArrayStringFlags();
    }
}