using System.Web.Mvc;
using DAL;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.All)]
    public class DefaultController : BaseController
    {
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }
    }
}