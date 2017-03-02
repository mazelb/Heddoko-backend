/**
 * @file PantsOctopi.cs
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
    public class PantsOctopi : BaseModel, IAuditable, ISoftDelete
    {
        [StringLength(255)]
        public string Location { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Label { get; set; }

        public string Notes { get; set; }

        public SizeType Size { get; set; }

        public EquipmentStatusType Status { get; set; }

        public PantsOctopiQAStatusType QAStatus { get; set; }

        #region Relations

        [JsonIgnore]
        //Inverse property - 1 to 1 relation, cause of ef6 1 to 1 supporting
        public virtual ICollection<Pants> PantsCollection { get; set; }

        [JsonIgnore]
        public virtual Pants Pants => PantsCollection?.FirstOrDefault();

        #endregion

        #region NotMapped
        bool ISoftDelete.IsDeleted => Status == EquipmentStatusType.Trash;

        public string IDView => $"PO{Id.ToString(Constants.PadZero)}";

        #endregion
    }
}