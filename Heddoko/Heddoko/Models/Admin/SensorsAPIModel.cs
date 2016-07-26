using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class SensorsAPIModel : BaseAPIModel
    {
        public SensorsAPIModel()
        {
        }

        public SensorsAPIModel(bool isEmpty)
        {
            this.IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; set; }

        public string IDView { get; set; }

        public SensorType Type { get; set; }

        public string version { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        public int? FirmwareID { get; set; }

        public EquipmentStatusType Status { get; set; }
        
        public SensorsQAStatusType QAStatus { get; set; }
        
        public string SetID { get; set; }

        public AnatomicLocationType? AnatomicalPosition { get; set; } 
    }
}