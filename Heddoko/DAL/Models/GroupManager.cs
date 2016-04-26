using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GroupManager : BaseModel
    {
        #region Relations
        [JsonIgnore]
        public int GroupID { get; set; }

        [JsonIgnore]
        public Group Group { get; set; }

        [JsonIgnore]
        public int ManagerID { get; set; }

        public User Manager { get; set; }
        #endregion
    }
}
