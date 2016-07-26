using System.Collections.Generic;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class SensorSet : BaseModel
    {
        public EquipmentStatusType Status { get; set; }

        public SensorsQAStatusType QAStatus { get; set; }

        #region NotMapped

        public string IDView => $"SS{ID.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kit { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sensor> Sensors { get; set; }

        #endregion
    }
}