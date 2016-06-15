using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Brainpack : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        public EquipmentStatusType Status { get; set; }

        public BrainpacksQAStatusType QAStatus { get; set; }

        #region Relations

        [JsonIgnore]
        public virtual Kit Kit { get; set; }

        [JsonIgnore]
        public virtual Powerboard Powerboard { get; set; }

        [JsonIgnore]
        public virtual Databoard Databoard { get; set; }
        #endregion
    }
}
