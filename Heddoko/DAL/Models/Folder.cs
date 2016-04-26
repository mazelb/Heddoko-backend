using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Folder : BaseModel
    {
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string SystemName { get; set; }

        [StringLength(255)]
        public string Path { get; set; }

        #region Relations
        public int? ParentID { get; set; }

        [JsonIgnore]
        public virtual Folder Parent { get; set; }

        public int ProfileID { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; }

        [JsonIgnore]
        public virtual ICollection<Folder> Children { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movement> Movements { get; set; }
        #endregion
    }
}
