using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Component : BaseModel, IAuditable, ISoftDelete
    {
        [StringLength(255)]
        public string Location { get; set; }

        public ComponentsType Type { get; set; }

        public EquipmentStatusType Status { get; set; }

        public int Quantity { get; set; }

        #region NotMapped
        bool ISoftDelete.IsDeleted => Status == EquipmentStatusType.Trash;

        public string IDView => $"CO{Id.ToString(Constants.PadZero)}";

        #endregion
    }
}