using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.Admin)]
    public class InventoryController : BaseController
    {
        public ActionResult Index()
        {
            InventoryViewModel model = new InventoryViewModel
            {
                EnableKendo = true
            };

            model.Assemblies = Services.AssembliesManager.GetAssemblies();

            return View(model);
        }
        

    }
}