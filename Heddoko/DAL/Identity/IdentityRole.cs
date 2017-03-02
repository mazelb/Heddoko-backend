/**
 * @file IdentityRole.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class IdentityRole : IdentityRole<int, UserRole>
    {
        public IdentityRole() { }
        public IdentityRole(string name) { Name = name; }
    }
}
