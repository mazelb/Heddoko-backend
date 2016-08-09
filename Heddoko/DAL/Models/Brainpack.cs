using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Brainpack : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public EquipmentStatusType Status { get; set; }

        public BrainpackQAStatusType QAStatus { get; set; }

        #region NotMapped

        public string IDView => $"BP{ID.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kits { get; set; }

        [JsonIgnore]
        public virtual Kit Kit => Kits?.FirstOrDefault();

        public int? PowerboardID { get; set; }

        [JsonIgnore]
        public virtual Powerboard Powerboard { get; set; }

        public int? DataboardID { get; set; }

        [JsonIgnore]
        public virtual Databoard Databoard { get; set; }

        [JsonIgnore]
        public int? FirmwareID { get; set; }

        [JsonIgnore]
        public virtual Firmware Firmware { get; set; }

        #endregion
    }
}