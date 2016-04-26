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

        public string Url
        {
            get
            {
                return string.IsNullOrEmpty(Image) ? null : $"{AssetsServer}/{Image}";
            }
        }
    }
}
