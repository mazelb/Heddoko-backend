using System.ComponentModel.DataAnnotations;
using Jil;
using Newtonsoft.Json;

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
        [JilDirective(Ignore = true)]
        public User User { get; set; }

        #endregion
    }
}