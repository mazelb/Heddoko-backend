using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Firmware : BaseModel
    {
        [StringLength(255)]
        public string Version { get; set; }

        public FirmwareType Type { get; set; }

        public FirmwareStatusType Status { get; set; }

        #region NotMapped

        public string IDView => $"FW{ID.ToString(Constants.PadZero)}";

        public string Url => Asset?.Url;

        #endregion

        #region Relations

        [JsonIgnore]
        public int? AssetID { get; set; }

        [JsonIgnore]
        public virtual Asset Asset { get; set; }

        #endregion
    }
}