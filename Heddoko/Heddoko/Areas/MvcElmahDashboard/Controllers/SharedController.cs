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