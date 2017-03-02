/**
 * @file AccessToken.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class AccessToken : BaseModel
    {
        [JsonIgnore]
        [StringLength(100)]
        public string Token { get; set; }

        #region Relations

        [JsonIgnore]
        public int UserID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public User User { get; set; }

        #endregion
    }
}