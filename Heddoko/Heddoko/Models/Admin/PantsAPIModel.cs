﻿using System.ComponentModel.DataAnnotations;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class PantsAPIModel : BaseAPIModel
    {
        public PantsAPIModel()
        {
        }

        public PantsAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        public int? PantsOctopiID { get; set; }

        public PantsOctopi PantsOctopi { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Pants}" : $"{IDView}";

    }
}