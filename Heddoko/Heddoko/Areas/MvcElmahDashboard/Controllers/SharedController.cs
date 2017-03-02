/**
 * @file SharedController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Web.Http.Description;
using System.Web.Mvc;

namespace Heddoko.Areas.MvcElmahDashboard.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SharedController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }
    }
}