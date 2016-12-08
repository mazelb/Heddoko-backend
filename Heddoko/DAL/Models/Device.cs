using System.ComponentModel.DataAnnotations;
using DAL.Models.Enum;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Device : BaseModel, IAuditable
    {
        [StringLength(255)]
        public string Token { get; set; }

        public DeviceType Type { get; set; }

        public DeviceStatus Status { get; set; }

        #region Relations
        public int UserID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public User User { get; set; }
        #endregion
    }
}
