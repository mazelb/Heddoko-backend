/**
 * @file ApplicationApiController.cs
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
using System.Text;

namespace Heddoko.Controllers.API
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/v1/applications")]
    [AuthAPI(Roles = Constants.Roles.All)]
    public class ApplicationApiController : BaseAdminController<Application, ApplicationAPIViewModel>
    {
        private const int secretLength = 24;

        private const int clientLength = 10;
        public ApplicationApiController() { }

        public ApplicationApiController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<ApplicationAPIViewModel>> Get([FromUri] KendoRequest request)
        {
            int count = 0;
            IEnumerable<Application> items = UoW.ApplicationRepository.GetByUserId(CurrentUser.Id, request.Take.Value, request.Skip.Value);          

            List<ApplicationAPIViewModel> itemsDefault = new List<ApplicationAPIViewModel>();

            count = items.Count();
            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<ApplicationAPIViewModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<ApplicationAPIViewModel> Post(ApplicationAPIViewModel model)
        {
            ApplicationAPIViewModel response;

            if (ModelState.IsValid)
            {                         
                Application item = new Application();
                item.Enabled = false;
                item.Client = GetHash(clientLength);
                item.Secret = GetHash(secretLength);

                Bind(item, model);
                UoW.ApplicationRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<ApplicationAPIViewModel>
            {
                Response = response
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
                        Bind(item, model);

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

        protected override Application Bind(Application item, ApplicationAPIViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Name = model.Name;
            item.UserID = CurrentUser.Id;
            item.RedirectUrl = model.RedirectUrl;

            return item;
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
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[length];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }
    }
}