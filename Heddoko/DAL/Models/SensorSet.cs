using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class SensorSet : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public EquipmentStatusType Status { get; set; }

        public SensorSetQAStatusType? QAStatus { get; set; }

        #region NotMapped

        public string IDView => $"SS{ID.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kits { get; set; }

        public virtual Kit Kit => Kits?.FirstOrDefault();

        [JsonIgnore]
        public virtual ICollection<Sensor> Sensors { get; set; }

        #endregion
    }
}