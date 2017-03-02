/**
 * @file ItemDetailsModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace Heddoko.Areas.MvcElmahDashboard.Models.Logs
{
    public class ItemDetailsModel
    {
        public Code.ElmahError Item { get; set; }

        public dynamic UserAgentInfoProvider { get; set; }

        public dynamic RemoteAddressInfoProvider { get; set; }
    }
}