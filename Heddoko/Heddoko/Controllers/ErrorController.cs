using DAL.Models;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string url)
        {
            ErrorViewModel model = new ErrorViewModel();
            model.Url = getUrl(url);
            model.Ex = ContextSession.LastError;

            model.Flash.Add(new FlashMessage()
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
            Response.StatusCode = 401;
            return View(model);
        }

        private string getUrl(string url)
        {
            return url ?? Request.QueryString["aspxerrorpath"] ?? Request.Url.OriginalString;
        }
    }
}