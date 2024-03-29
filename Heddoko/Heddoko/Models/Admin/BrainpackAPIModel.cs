﻿/**
 * @file BrainpackAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class BrainpackAPIModel : BaseAPIModel
    {
        public BrainpackAPIModel()
        {
        }

        public BrainpackAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Version { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Label { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Notes { get; set; }

        public EquipmentStatusType Status { get; set; }

        public BrainpackQAStatusType? QAStatus { get; set; }

        public Dictionary<string, bool> QaStatuses { get; set; }

        public int? FirmwareID { get; set; }

        public Firmware Firmware { get; set; }

        public int? PowerboardID { get; set; }

        public Powerboard Powerboard { get; set; }

        public int? DataboardID { get; set; }

        public Databoard Databoard { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Brainpack}" : $"{IDView} - {Label}";

        public string QAStatusText => QAStatus?.ToStringFlags();

        public List<string> QAModel => QAStatus?.ToArrayStringFlags();
    }
}