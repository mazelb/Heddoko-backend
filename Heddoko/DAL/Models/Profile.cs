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
    public class Profile : BaseModel
    {
        public Profile()
        {
            Gender = UserGenderType.NotSpecified;
        }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }

        public DateTime? BirthDay { get; set; }

        public UserGenderType Gender { get; set; }

        [Column(TypeName = "ntext")]
        public string Data { get; set; }

        #region Relations
        [JsonIgnore]
        public int? TagID { get; set; }

        [JsonIgnore]
        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }

        [JsonIgnore]
        public int? AssetID { get; set; }

        [JsonIgnore]
        public virtual Asset Asset { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Tag> Tags { get; set; }

        [JilDirective(Ignore = true)]
        public virtual ICollection<Group> Groups { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<User> Managers { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Movement> Movements { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Folder> Folders { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Screening> Screenings { get; set; }
        #endregion

        #region NotMapped
        public object Meta
        {
            get
            {
                return new
                {
                    Height = Height,
                    Mass = Weight,
                    Dob = BirthDay,
                    Gender = Gender,
                    Phone = Phone,
                    Email = Email,
                    Data = Data
                };
            }
        }
        public string AvatarSrc
        {
            get
            {
                return Asset == null? string.Empty : Asset.Url;
            }
        }

        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        #endregion
    }
}
