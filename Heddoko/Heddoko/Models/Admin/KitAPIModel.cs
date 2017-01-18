/**
 * @file KitAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class KitAPIModel : BaseAPIModel
    {
        public KitAPIModel()
        {
        }

        public KitAPIModel(bool isEmpty)
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

        public EquipmentStatusType Status { get; set; }

        public KitQAStatusType QAStatus { get; set; }

        public KitCompositionType Composition { get; set; }

        public int? BrainpackID { get; set; }

        public Brainpack Brainpack { get; set; }

        public int? SensorSetID { get; set; }

        public SensorSet SensorSet { get; set; }

        public int? PantsID { get; set; }

        public Pants Pants { get; set; }

        public int? ShirtID { get; set; }

        public Shirt Shirt { get; set; }

        public int? OrganizationID { get; set; }

        public Organization Organization { get; set; }

        public int? UserID { get; set; }

        public User User { get; set; }

        public string IDView { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.Kit}" : $"{IDView}";
    }
}