using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Pants : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        public int Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public SuitsQAStatusType QAStatus { get; set; }

        #region Relations
        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kit { get; set; }

        public int? PantsOctopiID { get; set; }

        [JsonIgnore]
        public virtual PantsOctopi PantsOctopi { get; set; }
        #endregion
    }
}
