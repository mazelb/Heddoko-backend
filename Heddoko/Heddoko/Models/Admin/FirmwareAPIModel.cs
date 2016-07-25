using System.ComponentModel.DataAnnotations;
using DAL.Models;
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

        public string Name
        {
            get { return IsEmpty ? $"{Resources.No} {Resources.SoftwareOrFirmware}" : $"{Version}"; }
        }
    }
}