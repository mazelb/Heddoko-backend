using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Kit : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        public KitCompositionType Composition { get; set; }

        public EquipmentStatusType Status { get; set; }

        #region Relations

        [JsonIgnore]
        public virtual Company Company { get; set; }

        [JsonIgnore]
        public virtual Brainpack Brainpack { get; set; }

        [JsonIgnore]
        public virtual SensorSet SensorSet { get; set; }

        [JsonIgnore]
        public virtual Shirt Shirt { get; set; }

        [JsonIgnore]
        public virtual Pants Pants { get; set; }
        #endregion
    }
}
