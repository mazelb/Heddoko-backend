using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class AssetAPIViewModel
    {
        public int? KitID { get; set; }

        public AssetType Type { get; set; }
    }
}