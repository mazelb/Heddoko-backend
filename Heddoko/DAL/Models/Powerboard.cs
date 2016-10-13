using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Powerboard : BaseModel, IAuditable, ISoftDelete
    {
        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        public string Notes { get; set; }

        public EquipmentStatusType Status { get; set; }

        public PowerboardQAStatusType QAStatus { get; set; }

        #region NotMapped
        bool ISoftDelete.IsDeleted => Status == EquipmentStatusType.Trash;

        public string IDView => $"PB{Id.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Brainpack> Brainpacks { get; set; }

        public virtual Brainpack Brainpack => Brainpacks?.FirstOrDefault();

        [JsonIgnore]
        public int? FirmwareID { get; set; }

        [JsonIgnore]
        public virtual Firmware Firmware { get; set; }

        #endregion
    }
}