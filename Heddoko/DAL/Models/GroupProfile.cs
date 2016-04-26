using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GroupProfile : BaseModel
    {
        #region Relations
        [JsonIgnore]
        public int GroupID { get; set; }

        [JsonIgnore]
        public Group Group { get; set; }

        [JsonIgnore]
        public int ProfileID { get; set; }

        public Profile Profile { get; set; }
        #endregion
    }
}
