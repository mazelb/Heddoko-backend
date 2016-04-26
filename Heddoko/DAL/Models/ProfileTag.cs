using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ProfileTag : BaseModel
    {
        #region Relations
        [JsonIgnore]
        public int ProfileID { get; set; }

        [JsonIgnore]
        public Profile Profile { get; set; }

        [JsonIgnore]
        public int TagID { get; set; }

        public Tag Tag { get; set; }
        #endregion
    }
}
