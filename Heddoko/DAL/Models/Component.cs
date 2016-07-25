using System.ComponentModel.DataAnnotations;

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

        #region NotMapped

        public string IDView => $"CO{ID.ToString(Constants.PadZero)}";

        #endregion
    }
}