﻿using Jil;
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
    public class Group : BaseModel
    {
        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Meta { get; set; }

        #region Relations
        [JsonProperty("mainTagId")]
        public int? TagID { get; set; }

        [JsonIgnore]
        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }

        [JsonIgnore]
        public int? AssetID { get; set; }

        [JsonIgnore]
        public virtual Asset Asset { get; set; }

        [JilDirective(Ignore = true)]
        public virtual ICollection<Tag> Tags { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Profile> Profiles { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<User> Managers { get; set; }
        #endregion

        #region NotMapped 
        public string AvatarSrc
        {
            get
            {
                return Asset == null ? string.Empty : Asset.Url;
            }
        }
        #endregion
    }
}
