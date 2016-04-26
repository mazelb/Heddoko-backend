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
    public class ComplexEquipment : BaseModel
    {
        public ComplexEquipment()
        {
            Status = EquipmentStatusType.Available;
        }

        public EquipmentStatusType Status { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string MacAddress { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string SerialNo { get; set; }

        [StringLength(255)]
        public string PhysicalLocation { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        #region Relations
        [JsonIgnore]
        public virtual ICollection<Equipment> Equipments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movement> Movements { get; set; }
        #endregion
    }
}
