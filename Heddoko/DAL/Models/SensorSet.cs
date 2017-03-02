/**
 * @file SensorSet.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class SensorSet : BaseModel, IAuditable, ISoftDelete
    {
        [StringLength(255)]
        public string Location { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        public string Notes { get; set; }

        public EquipmentStatusType Status { get; set; }

        public SensorSetQAStatusType? QAStatus { get; set; }

        #region NotMapped
        bool ISoftDelete.IsDeleted => Status == EquipmentStatusType.Trash;

        public string IDView => $"SS{Id.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kits { get; set; }

        public virtual Kit Kit => Kits?.FirstOrDefault();

        [JsonIgnore]
        public virtual ICollection<Sensor> Sensors { get; set; }

        #endregion
    }
}