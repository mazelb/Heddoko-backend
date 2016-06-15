using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ShirtOctopi : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        public int Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public SuitsQAStatusType QAStatus { get; set; }

        #region Relations

        [JsonIgnore]
        public virtual Kit Kit { get; set; }

        [JsonIgnore]
        public virtual Shirt Shirt { get; set; }
        #endregion
    }
}
