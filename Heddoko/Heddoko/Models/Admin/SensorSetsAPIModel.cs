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

        public List<string> sensorIDs { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        public string KitID { get; set; }
    }
}