using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Shirt : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        #region NotMapped

        public string IDView => $"SH{ID.ToString(Constants.PadZero)}";

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