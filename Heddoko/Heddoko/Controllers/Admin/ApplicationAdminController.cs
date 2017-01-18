/**
 * @file ApplicationAdminController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Linq;
using System.Web.Http.Description;
using DAL;
using Services;
using System;
using System.Threading.Tasks;
using DAL.Models;
using Hangfire;
using Microsoft.AspNet.Identity.Owin;
using Heddoko.Models;
using Heddoko.Controllers;
using System.Security.Cryptography;

namespace Heddoko.Controllers.API
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/applications")]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public class ApplicationAdminController : BaseAdminController<Application, ApplicationAPIViewModel>
    {
        public ApplicationAdminController() { }

        public ApplicationAdminController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<ApplicationAPIViewModel>> Get([FromUri] KendoRequest request)
        {
            int count = 0;
            IEnumerable<Application> items = null;

            if (IsAdmin)
            {
                items = UoW.ApplicationRepository.All(request.Take.Value, request.Skip.Value);
            }           

            List<ApplicationAPIViewModel> itemsDefault = new List<ApplicationAPIViewModel>();

            count = items.Count();
            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<ApplicationAPIViewModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<ApplicationAPIViewModel> Put(ApplicationAPIViewModel model)
        {
            ApplicationAPIViewModel response = new ApplicationAPIViewModel();

            if (model.Id.HasValue)
            {
                Application item = UoW.ApplicationRepository.GetFull(model.Id.Value);
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (IsAdmin)
                        {
                            item.Enabled = model.Enabled;
                        }

                        UoW.Save();

                        response = Convert(item);
                    }
                    else
                    {
                        throw new ModelStateException
                        {
                            ModelState = ModelState
                        };
                    }
                }
            }

            return new KendoResponse<ApplicationAPIViewModel>
            {
                Response = response
            };
        }

        protected override ApplicationAPIViewModel Convert(Application item)
        {
            if (item == null)
            {
                return null;
            }

            return new ApplicationAPIViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Client = item.Client,
                Secret = item.Secret,
                Enabled = item.Enabled,
                RedirectUrl = item.RedirectUrl
            };
        }

        protected string GetHash(int length)
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[length];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string uniq = System.Convert.ToBase64String(buffer);

            return uniq;
        }
    }
}