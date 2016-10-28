using System.Web.Mvc;
using DAL.Models;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string url)
        {
            ErrorViewModel model = new ErrorViewModel
            {
                Url = GetUrl(url),
                Ex = ContextSession.LastError
            };

            model.Flash.Add(new FlashMessage
            {
                Type = FlashMessageType.Error,
                Message = model.ExMessage
            });
            Response.StatusCode = 500;
            return View(model);
        }

        public ActionResult NotFound(string url)
        {
            ErrorViewModel model = new ErrorViewModel();
            Response.StatusCode = 404;
            return View(model);
        }

        public ActionResult AccessDenied(string url)
        {
            ErrorViewModel model = new ErrorViewModel();
            Response.StatusCode = 403;
            return View(model);
        }

        private string GetUrl(string url)
        {
            return url ?? Request.QueryString["aspxerrorpath"] ?? Request?.Url?.OriginalString;
        }
    }
}