/**
 * @file InventoryController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Services;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.Admin)]
    public class InventoryController : BaseController
    {
        public InventoryController() : base() { }

        public InventoryController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

        public ActionResult Index()
        {
            InventoryViewModel model = new InventoryViewModel
            {
                EnableKendo = true,
                Assemblies = Services.AssembliesManager.GetAssemblies()
            };

            return View(model);
        }
        

    }
}