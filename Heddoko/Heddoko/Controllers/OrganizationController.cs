using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    [Auth(Roles = DAL.Constants.Roles.LicenseAdminAndAdmin)]
    public class OrganizationController : BaseController
    {
        public ActionResult Index()
        {
            OrganizationIndexViewModel model = new OrganizationIndexViewModel();

            if (!CurrentUser.OrganizationID.HasValue)
            {
                return RedirectToAction("Index", "Default");
            }

            model.Organization = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);
            model.Title = model.Organization.Name;
            model.EnableKendo = true;
            return View(model);
        }
    }
}