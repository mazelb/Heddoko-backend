using DAL.Models;

namespace Heddoko.Models.Admin
{
    public class AssetFileAPIModel
    {
        public AssetType Type { get; set; }

        public string FileName { get; set; }

        public string Url { get; set; }
    }
}