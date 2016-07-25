using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Sensor : BaseModel
    {
        public SensorType Type { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        [StringLength(255)]
        public string FirmwareVersion { get; set; }

        public EquipmentStatusType Status { get; set; }

        public SensorsQAStatusType QAStatus { get; set; }

        public AnatomicLocationType AnatomicLocation { get; set; }

        #region NotMapped

        public string IDView => $"SE{ID.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        public int? SensorSetID { get; set; }

        public virtual SensorSet SensorSet { get; set; }

        #endregion
    }
}