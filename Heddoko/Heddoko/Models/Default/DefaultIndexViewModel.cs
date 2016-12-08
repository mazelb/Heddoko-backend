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

        public double? UserErgoScore { get; set; }

        public bool HasUserScore => UserErgoScore.HasValue && UserErgoScore.Value != 0;
    }
}