using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Shirt : BaseModel
    {

        [StringLength(255)]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        #region Relations
        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kit { get; set; }

        public int? ShirtOctopiID { get; set; }

        [JsonIgnore]
        public virtual ShirtOctopi ShirtOctopi { get; set; }
        #endregion

        #region NotMapped
        public string IDView
        {
            get
            {
                return $"SH{ID.ToString(Constants.PadZero)}";
            }
        }
        #endregion
    }
}
