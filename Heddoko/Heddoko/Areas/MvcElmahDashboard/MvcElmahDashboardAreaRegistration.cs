/**
 * @file MvcElmahDashboardAreaRegistration.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Web.Mvc;

namespace Heddoko.Areas.MvcElmahDashboard
{
    public class MvcElmahDashboardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MvcElmahDashboard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "MvcElmahDashboard_default",
                url: "elmah-dashboard/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Heddoko.Areas.MvcElmahDashboard.Controllers" }
            );
        }
    }
}