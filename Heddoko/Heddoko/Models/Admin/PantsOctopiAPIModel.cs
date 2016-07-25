using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class PantsOctopiAPIModel : BaseAPIModel
    {
        public PantsOctopiAPIModel()
        {
        }

        public PantsOctopiAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        public string IDView { get; set; }

        public string Name
        {
            get { return IsEmpty ? $"{Resources.No} {Resources.PantsOctopi}" : $"{IDView} - {Size.GetDisplayName()} - {Location}"; }
        }
    }
}