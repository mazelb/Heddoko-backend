/**
 * @file UserRole.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        [ForeignKey("RoleId")]
        [JilDirective(Ignore = true)]
        [JsonIgnore]
        public virtual IdentityRole Role { get; set; }
    }
}
