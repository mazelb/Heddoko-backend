﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class DefaultIndexViewModel : BaseViewModel 
    {
        public Firmware Software { get; set; }
    }
}