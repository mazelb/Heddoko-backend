/**
 * @file SensorSetsAPIModel.cs
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
    public class SensorSetsAPIModel : BaseAPIModel
    {
        public SensorSetsAPIModel()
        {
        }

        public SensorSetsAPIModel(bool isEmpty)
        {
            this.IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; set; }

        public string IDView { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Location { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Label { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Notes { get; set; }

        public SensorSetQAStatusType? QAStatus { get; set; }

        public EquipmentStatusType Status { get; set; }

        public Kit Kit { get; set; }

        public int? KitID { get; set; }

        public List<int> Sensors { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.SensorSet}" : $"{IDView} - {Label}";
    }
}