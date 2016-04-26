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
    public class Screening : BaseModel
    {
        [StringLength(255)]
        public string Title { get; set; }
        
        public bool? Score { get; set; }

        public bool? ScoreMin { get; set; }

        public bool? ScoreMax { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Column(TypeName = "ntext")]
        public string Meta { get; set; }

        #region Relations
        public int? ProfileID { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movement> Movements { get; set; }
        #endregion
    }
}
