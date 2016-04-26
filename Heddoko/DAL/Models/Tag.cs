using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Tag : BaseModel
    {
        [StringLength(100)]
        public string Title { get; set; }

        #region Relations
        [JsonIgnore]
        public virtual ICollection<ProfileTag> Profiles { get; set; }

        [JsonIgnore]
        public virtual ICollection<MovementTag> Movements { get; set; }

        [JsonIgnore]
        public virtual ICollection<GroupTag> Groups { get; set; }
        #endregion
    }
}
