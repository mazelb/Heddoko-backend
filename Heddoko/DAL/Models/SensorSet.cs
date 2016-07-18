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
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kit { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sensor> Sensors { get; set; }
        #endregion

        #region NotMapped
        public string IDView
        {
            get
            {
                return $"SS{ID.ToString(Constants.PadZero)}";
            }
        }
        #endregion
    }
}
