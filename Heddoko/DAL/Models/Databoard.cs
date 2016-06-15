using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Databoard : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        [StringLength(255)]
        public string FirmwareVersion { get; set; }

        public EquipmentStatusType Status { get; set; }

        #region Relations

        [JsonIgnore]
        public virtual Brainpack Brainpack { get; set; }
        #endregion
    }
}
