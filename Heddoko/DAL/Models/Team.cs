/**
 * @file Team.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Team : BaseModel
    {
        [Index]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public string Notes { get; set; }

        public TeamStatusType Status { get; set; }

        #region Relations

        [JsonIgnore]
        public int OrganizationID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<User> Users { get; set; }
        #endregion

        #region NotMapped
        public string IDView => $"TM{Id.ToString(Constants.PadZero)}";
        #endregion
    }
}