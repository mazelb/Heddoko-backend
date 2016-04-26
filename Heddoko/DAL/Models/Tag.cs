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
        public virtual ICollection<Profile> Profiles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movement> Movements { get; set; }

        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; }
        #endregion
    }
}
