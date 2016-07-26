﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Asset : BaseModel
    {
        [StringLength(255)]
        [Index]
        [JsonIgnore]
        public string Image { get; set; }

        public AssetType Type { get; set; }

        [JsonIgnore]
        public UploadStatusType Status { get; set; }

        [JsonIgnore]
        public AssetProccessingType Proccessing { get; set; }

        public string Url
        {
            get
            {
                if (!string.IsNullOrEmpty(Image))
                {
                    return Image.StartsWith("http") ? Image : $"{AssetsServer}{Image}";
                }
                return null;
            }
        }

        #region Relations

        public int? UserID { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public int? KitID { get; set; }

        [JsonIgnore]
        public virtual Kit Kit { get; set; }

        #endregion
    }
}