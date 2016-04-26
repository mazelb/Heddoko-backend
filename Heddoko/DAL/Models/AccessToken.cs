using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AccessToken : BaseModel
    {
        [JsonIgnore]
        [StringLength(100)]
        public string Token { get; set; }

        #region Relations
        [JsonIgnore]
        public int UserID { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        #endregion
    }
}
