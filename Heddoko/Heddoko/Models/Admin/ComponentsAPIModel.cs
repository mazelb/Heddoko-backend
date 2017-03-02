/**
 * @file ComponentsAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ComponentsAPIModel : BaseAPIModel
    {
        public EquipmentStatusType Status { get; set; }

        public int Quantity { get; set; }

        public ComponentsType Type { get; set; }

        public string Location { get; set; }
    }
}