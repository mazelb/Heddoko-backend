using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AnatomicalPositionType AnatomicalPosition { get; set; }

        #region Relations

        [JsonIgnore]
        public virtual SensorSet SensorSet { get; set; }
        #endregion
    }
}
