using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Component : BaseModel
    {
        //TODO check if we need that field
        [StringLength(255)]
        public string Location { get; set; }

        public ComponentsType Type { get; set; }

        public EquipmentStatusType Status { get; set; }

        public int Quantity { get; set; }
    }
}
