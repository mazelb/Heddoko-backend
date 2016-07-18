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
    public class Brainpack : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        public EquipmentStatusType Status { get; set; }

        public BrainpacksQAStatusType QAStatus { get; set; }

        #region Relations
        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kit { get; set; }

        public int? PowerboardID {get;set;}

        [JsonIgnore]
        public virtual Powerboard Powerboard { get; set; }

        public int? DataboardID { get; set; }

        [JsonIgnore]
        public virtual Databoard Databoard { get; set; }
        #endregion

        #region NotMapped
        public string IDView
        {
            get
            {
                return $"BP{ID.ToString(Constants.PadZero)}";
            }
        }
        #endregion
    }
}
