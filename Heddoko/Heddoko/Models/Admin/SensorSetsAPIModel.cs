﻿using System.ComponentModel.DataAnnotations;
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

        public SensorsQAStatusType QAStatus { get; set; }

        public ICollection<Kit> Kit { get; set; }

        public string Sensors { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.SensorSet}" : $"{IDView} - {QAStatus}";
    }
}