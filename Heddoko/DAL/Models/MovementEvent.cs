using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MovementEvent : BaseModel
    {
        [Column(TypeName = "ntext")]
        public string Data { get; set; }

        public MovementEventType Type { get; set; }

        #region Relations
        public int MovementID { get; set; }

        [JsonIgnore]
        public virtual Movement Movement { get; set; }

        public int? StartFrameID { get; set; }

        [JsonIgnore]
        public virtual MovementFrame StartFrame { get; set; }

        public int? EndFrameID { get; set; }

        [JsonIgnore]
        public virtual MovementFrame EndFrame { get; set; }
        #endregion
    }
}
