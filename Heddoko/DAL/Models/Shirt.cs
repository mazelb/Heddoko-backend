using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Shirt : BaseModel, IAuditable, ISoftDelete
    {
        [StringLength(255)]
        public string Location { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        public string Notes { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public ShirtQAStatusType QAStatus { get; set; }

        #region NotMapped

        bool ISoftDelete.IsDeleted => Status == EquipmentStatusType.Trash;

        public string IDView => $"SH{Id.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kit { get; set; }

        public int? ShirtOctopiID { get; set; }

        [JsonIgnore]
        public virtual ShirtOctopi ShirtOctopi { get; set; }

        #endregion
    }
}