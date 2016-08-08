﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class PantsOctopi : BaseModel
    {
        [StringLength(255)]
        public string Location { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public EquipmentQAStatusType QAStatus { get; set; }

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Pants> PantsCollection { get; set; }

        [JsonIgnore]
        public virtual Pants Pants => PantsCollection?.FirstOrDefault();

        #endregion

        #region NotMapped

        public string IDView => $"PO{ID.ToString(Constants.PadZero)}";

        #endregion
    }
}