/**
 * @file OrganizationController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Web.Mvc;
using DAL;
using Heddoko.Models;
using Services;

namespace Heddoko.Controllers
{
    //[Auth(Roles = Constants.Roles.LicenseAdminAndAnalystAndAdmin)]
    public class OrganizationController : BaseController
    {
        public OrganizationController() : base() { }


        public OrganizationController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

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