using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Asset : BaseModel
    {
        [StringLength(255)]
        [Index]
        [JsonIgnore]
        public string Image { get; set; }

        [JsonIgnore]
        public AssetType Type { get; set; }

        [JsonIgnore]
        public UploadStatusType Status { get; set; }

        public string Url
        {
            get
            {
                if (!string.IsNullOrEmpty(Image))
                {
                    return Image.StartsWith("http") ? Image : $"{AssetsServer}/{Image}";
                }
                return null;
            }
        }
    }
}
