using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MovementFrame : BaseModel
    {
        [StringLength(255)]
        public string Revision { get; set; }

        #region Relations
        [JsonIgnore]
        public int MovementID { get; set; }

        public Movement Movement { get; set; }
        #endregion
    }
}
