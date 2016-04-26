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
    public class Group : BaseModel
    {
        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Meta { get; set; }

        #region Relations
        public int? TagID { get; set; }

        [JsonIgnore]
        public virtual Tag Tag { get; set; }

        public int? AssetID { get; set; }

        [JsonIgnore]
        public virtual Asset Asset { get; set; }

        [JsonIgnore]
        public virtual ICollection<GroupTag> Tags { get; set; }

        [JsonIgnore]
        public virtual ICollection<GroupProfile> Profiles { get; set; }

        [JsonIgnore]
        public virtual ICollection<GroupManager> Managers { get; set; }
        #endregion
    }
}
