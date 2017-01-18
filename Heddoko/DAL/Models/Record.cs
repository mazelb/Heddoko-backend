/**
 * @file Record.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models.Enum;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Record : BaseModel, IAuditable
    {
        public RecordType Type { get; set; }

        public int? UserID { get; set; }

        public virtual User User { get; set; }

        public int? KitID { get; set; }

        [JsonIgnore]
        public virtual Kit Kit { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
