/**
 * @file IndexModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace Heddoko.Areas.MvcElmahDashboard.Models.Logs
{
    public class IndexModel
    {
        public string[] Applications { get; set; }

        public string[] Hosts { get; set; }
        
        public string[] Types { get; set; }
        
        public string[] Sources { get; set; }
    }
}