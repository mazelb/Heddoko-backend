using System.Collections.Generic;

namespace Heddoko.Models.API
{
    public class RecordAPIViewModel
    {
        public int? KitID { get; set; }

        public string Label { get; set; }

        public List<AssetFileAPIViewModel> FileTypes { get; set; }
    }
}