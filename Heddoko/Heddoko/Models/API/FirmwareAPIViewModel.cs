using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class FirmwareAPIViewModel
    {
        public int? ID { get; set; }

        public int? FirmwareID { get; set; }

        public FirmwareType? Type { get; set; }
    }
}