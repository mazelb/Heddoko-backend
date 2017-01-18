/**
 * @file InventoryViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using DAL.Models;

namespace Heddoko.Models
{
    public class InventoryViewModel : BaseViewModel
    {
        public List<Assembly> Assemblies { get; set; }
    }
}