﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Pants : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        #region NotMapped

        public string IDView => $"PA{ID.ToString(Constants.PadZero)}";

        #endregion

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Kit> Kit { get; set; }

        public int? PantsOctopiID { get; set; }

        [JsonIgnore]
        public virtual PantsOctopi PantsOctopi { get; set; }

        #endregion
    }
}