using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MovementMarker : BaseModel
    {
        [StringLength(255)]
        public string Comments { get; set; }

        #region Relations
        public int? StartFrameID { get; set; }

        [JsonIgnore]
        public virtual MovementFrame StartFrame { get; set; }

        public int? EndFrameID { get; set; }

        [JsonIgnore]
        public virtual MovementFrame EndFrame { get; set; }
        #endregion
    }
}
