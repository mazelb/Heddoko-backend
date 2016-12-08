using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DAL.Models;
using Heddoko.Models.Admin;
using i18n;

namespace Heddoko.Models
{
    public class FirmwareAPIModel : BaseAPIModel
    {
        public FirmwareAPIModel()
        {
        }

        public FirmwareAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [StringLength(255, ErrorMessageResourceName = "ValidateLengthRangeMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Version { get; set; }

        public FirmwareType Type { get; set; }

        public FirmwareStatusType Status { get; set; }

        public string IDView { get; set; }

        public string Url { get; set; }

        public string Name => IsEmpty ? $"{Resources.No} {Resources.SoftwareOrFirmware}" : $"{Version}";

        public List<AssetFileAPIModel> Files { get; set; }
    }
}