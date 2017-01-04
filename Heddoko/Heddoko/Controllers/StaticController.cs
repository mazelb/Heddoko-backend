using System.Web.Mvc;
using Heddoko.Models;
using Services;
using DAL;
using Heddoko.Helpers.DomainRouting.Mvc;

namespace Heddoko.Controllers
{
    public class StaticController : BaseController
    {
        public StaticController() : base() { }

        public StaticController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

        public ActionResult Terms()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Privacy()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Requirements()
        {
            return View(new BaseViewModel());
        }

        [DomainRoute("", Constants.ConfigKeyName.PublicApiSite)]
        public ActionResult Help()
        {
            return View(new BaseViewModel());
        }
    }
}