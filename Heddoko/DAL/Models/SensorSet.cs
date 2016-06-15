using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SensorSet : BaseModel
    {
        public SensorsQAStatusType QAStatus { get; set; }

        #region Relations

        [JsonIgnore]
        public virtual Kit Kit { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sensor> Sensors { get; set; }
        #endregion
    }
}
