using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? OrganizationID { get; set; }

        [JsonIgnore]
        public virtual Organization Organization { get; set; }

        public int? BrainpackID { get; set; }

        [JsonIgnore]
        public virtual Brainpack Brainpack { get; set; }

        public int? SensorSetID { get; set; }

        [JsonIgnore]
        public virtual SensorSet SensorSet { get; set; }

        public int? ShirtID { get; set; }

        [JsonIgnore]
        public virtual Shirt Shirt { get; set; }

        public int? PantsID { get; set; }

        [JsonIgnore]
        public virtual Pants Pants { get; set; }
        #endregion
    }
}
