using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Kit : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        public KitCompositionType Composition { get; set; }

        public EquipmentStatusType Status { get; set; }

        #region NotMapped

        public string IDView => $"KI{ID.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        public int? OrganizationID { get; set; }

        [JsonIgnore]
        public virtual Organization Organization { get; set; }

        public int? UserID { get; set; }

        public virtual User User { get; set; }

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