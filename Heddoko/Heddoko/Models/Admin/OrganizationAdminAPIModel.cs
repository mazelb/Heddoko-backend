/**
 * @file OrganizationAdminAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class OrganizationAdminAPIModel
    {
        public int UserID { get; set; }

        public int OrganizationID { get; set; }
    }
}