using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Kit : BaseModel, IAuditable, ISoftDelete
    {
        [StringLength(255)]
        [JsonIgnore]
        public string Location { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        [JsonIgnore]
        public string Notes { get; set; }

        [JsonIgnore]
        public KitCompositionType Composition { get; set; }

        [JsonIgnore]
        public EquipmentStatusType Status { get; set; }

        [JsonIgnore]
        public KitQAStatusType QAStatus { get; set; }

        #region NotMapped
        bool ISoftDelete.IsDeleted => Status == EquipmentStatusType.Trash;

        public string IDView => $"KI{Id.ToString(Constants.PadZero)}";

        #endregion

        #region Relations
        [JsonIgnore]
        public int? OrganizationID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        public int? UserID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual User User { get; set; }

        [JsonIgnore]
        public int? BrainpackID { get; set; }

        public virtual Brainpack Brainpack { get; set; }

        [JsonIgnore]
        public int? SensorSetID { get; set; }

        [JsonIgnore]
        public virtual SensorSet SensorSet { get; set; }

        [JsonIgnore]
        public int? ShirtID { get; set; }

        [JsonIgnore]
        public virtual Shirt Shirt { get; set; }

        [JsonIgnore]
        public int? PantsID { get; set; }

        [JsonIgnore]
        public virtual Pants Pants { get; set; }

        #endregion
    }
}