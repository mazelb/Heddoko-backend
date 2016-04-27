using Heddoko.Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    [Auth(Roles = DAL.Constants.Roles.All)]
    public class DefaultController : BaseController
    {
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [AllowAnonymous]
        public ActionResult SeedUpload()
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(Server.MapPath("~/Content/seed")));
            foreach (string file in files)
            {
                Azure.Upload($"seed/{Path.GetFileName(file)}", Config.AssetsContainer, file);
            }
            return View(new BaseViewModel());
        }
    }
}