/**
 * @file Firmware.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Firmware : BaseModel, IAuditable
    {
        [StringLength(255)]
        public string Version { get; set; }

        public FirmwareType Type { get; set; }

        public FirmwareStatusType Status { get; set; }

        #region NotMapped
        public string IDView => $"FW{Id.ToString(Constants.PadZero)}";

        public string Url => Asset?.Url;

        public string Name => Asset?.Name;

        #endregion

        #region Relations

        [JsonIgnore]
        public int? AssetID { get; set; }

        [JsonIgnore]
        public virtual Asset Asset { get; set; }

        [JsonIgnore]
        public int? RecordID { get; set; }

        [JsonIgnore]
        public virtual Record Record { get; set; }

        #endregion
    }
}