/**
 * @file Application.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Application : BaseModel, IAuditable
    {
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Client { get; set; }

        [StringLength(255)]
        public string Secret { get; set; }

        [StringLength(2048)]
        public string RedirectUrl { get; set; }       

        public bool Enabled { get; set; }

        #region NotMapped

        #endregion

        #region Relations

        [JsonIgnore]
        public int UserID { get; set; }

        #endregion
    }
}