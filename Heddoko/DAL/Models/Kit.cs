﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Kit : BaseModel
    {
        [StringLength(255)]
        [JsonIgnore]
        public string Location { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        [Column(TypeName = "ntext")]
        [JsonIgnore]
        public string Notes { get; set; }

        [JsonIgnore]
        public KitCompositionType Composition { get; set; }

        [JsonIgnore]
        public EquipmentStatusType Status { get; set; }

        [JsonIgnore]
        public KitQAStatusType QAStatus { get; set; }

        #region NotMapped

        public string IDView => $"KI{ID.ToString(Constants.PadZero)}";

        #endregion

        #region Relations
        [JsonIgnore]
        public int? OrganizationID { get; set; }

        [JsonIgnore]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        public int? UserID { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public int? BrainpackID { get; set; }

        [JsonIgnore]
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