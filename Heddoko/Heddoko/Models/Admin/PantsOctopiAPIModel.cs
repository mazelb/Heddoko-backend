using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DAL;


namespace Heddoko.Models
{
    public class PantsOctopiAPIModel : BaseAPIModel
    {
        private bool IsEmpty { get; set; }
        public PantsOctopiAPIModel()
        {

        }

        public PantsOctopiAPIModel(bool isEmpty)
        {
            this.IsEmpty = isEmpty;
        }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        public string IDView { get; set; }

        public string Name
        {
            get
            {
                return IsEmpty ? $"{i18n.Resources.No} {i18n.Resources.PantsOctopi}" : $"{IDView} - {Size.GetDisplayName()} - {Location}";
            }
        }
    }
}