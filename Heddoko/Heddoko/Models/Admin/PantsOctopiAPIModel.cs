﻿/**
 * @file PantsOctopiAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;
using System.Collections.Generic;

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

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Label { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Notes { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public PantsOctopiQAStatusType? QAStatus { get; set; }

        public Dictionary<string, bool> QaStatuses { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.PantsOctopi}" : $"{IDView} - {Label}";

        public string QAStatusText => QAStatus?.ToStringFlags();

        public List<string> QAModel => QAStatus?.ToArrayStringFlags();
    }
}
