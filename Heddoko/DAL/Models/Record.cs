using System.Collections.Generic;
using DAL.Models.Enum;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Record : BaseModel, IAuditable
    {
        public RecordType Type { get; set; }

        public int? UserID { get; set; }

        public virtual User User { get; set; }

        public int? KitID { get; set; }

        [JsonIgnore]
        public virtual Kit Kit { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
