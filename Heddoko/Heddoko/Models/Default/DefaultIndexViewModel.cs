/**
 * @file DefaultIndexViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class DefaultIndexViewModel : BaseViewModel 
    {
        public Firmware Software { get; set; }

        public Firmware Guide { get; set; }
    }
}