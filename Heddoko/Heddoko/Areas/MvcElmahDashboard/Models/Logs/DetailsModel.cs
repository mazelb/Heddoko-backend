/**
 * @file DetailsModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Areas.MvcElmahDashboard.Models.Logs
{
    public class DetailsModel
    {
        public Guid Id { get; set; }

        public String Application { get; set; }
        
        public String Host { get; set; }
        
        public String Source { get; set; }
        
        public String Type { get; set; }
        
        public String Search { get; set; }

        public dynamic UserAgentInfoProvider { get; set; }

        public dynamic RemoteAddressInfoProvider { get; set; }
    }
}