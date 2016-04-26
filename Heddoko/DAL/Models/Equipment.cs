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
    public class Equipment : BaseModel
    {
        public Equipment()
        {
            Status = EquipmentStatusType.Available;
            AnatomicalPosition = AnatomicalPositionType.UpperSpine;
            Prototype = PrototypeType.Yes;
            Condition = ConditionType.New;
            Numbers = NumbersType.Yes;
            HeatsShrink = HeatsShrinkType.Yes;
            Ship = ShipType.No;
        }

        public EquipmentStatusType Status { get; set; }

        public AnatomicalPositionType? AnatomicalPosition { get; set; }

        public PrototypeType Prototype { get; set; }

        public ConditionType Condition { get; set; }

        public NumbersType Numbers { get; set; }

        public HeatsShrinkType HeatsShrink { get; set; }

        public ShipType Ship { get; set; }

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
        public int? ComplexEquipmentID { get; set; }

        [JsonIgnore]
        public virtual ComplexEquipment ComplexEquipment { get; set; }

        public int MaterialID { get; set; }

        [JsonIgnore]
        public virtual Material Material { get; set; }

        public int? VerifiedByID { get; set; }

        [JsonIgnore]
        public virtual User VerifiedBy { get; set; }
        #endregion
    }
}
