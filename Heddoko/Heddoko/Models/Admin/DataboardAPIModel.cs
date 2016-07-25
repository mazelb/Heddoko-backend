﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class DataboardAPIModel : BaseAPIModel
    {
        public DataboardAPIModel()
        {
        }

        public DataboardAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Version { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        public EquipmentStatusType Status { get; set; }

        public int? FirmwareID { get; set; }

        public Firmware Firmware { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Dashboard}" : $"{IDView}";
    }
}