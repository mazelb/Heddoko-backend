﻿/**
 * @file Sensor.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Sensor : BaseModel, IAuditable, ISoftDelete
    {
        public SensorType Type { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        public string Notes { get; set; }

        public EquipmentStatusType Status { get; set; }

        public SensorQAStatusType QAStatus { get; set; }

        public AnatomicalLocationType? AnatomicalLocation { get; set; }

        #region NotMapped
        bool ISoftDelete.IsDeleted => Status == EquipmentStatusType.Trash;

        public string IDView => $"SE{Id.ToString(Constants.PadZero)}";

        #endregion

        #region Relations
        [JsonIgnore]
        public int? FirmwareID { get; set; }

        [JsonIgnore]
        public virtual Firmware Firmware { get; set; }

        [JsonIgnore]
        public int? SensorSetID { get; set; }

        public virtual SensorSet SensorSet { get; set; }

        #endregion
    }
}