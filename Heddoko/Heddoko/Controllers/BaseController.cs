using System.Web.Http.Description;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : Controller
    {
        public BaseController()
        {
            UoW = new UnitOfWork();
            CurrentUser = Forms.ValidateSession(UoW);
            ContextTempData = new ContextTempData(TempData);
        }

        protected UnitOfWork UoW { get; private set; }

        protected ContextTempData ContextTempData { get; private set; }

        protected User CurrentUser { get; set; }

        protected bool IsAuth => CurrentUser != null;
    }
}