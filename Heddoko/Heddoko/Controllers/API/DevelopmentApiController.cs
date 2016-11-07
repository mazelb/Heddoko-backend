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
    [RoutePrefix("api/v1/developments")]
    [AllowAnonymous]
    public class DevelopmentApiController : BaseAdminController<Development, DevelopmentAPIViewModel>
    {
        private const int secretLength = 24;

        private const int clientLength = 10;
        public DevelopmentApiController() { }

        public DevelopmentApiController(ApplicationUserManager userManager, UnitOfWork uow): base(userManager, uow) { }

        public override KendoResponse<IEnumerable<DevelopmentAPIViewModel>> Get([FromUri] KendoRequest request)
        {
            int count = 0;
            IEnumerable<Development> items = null;

            if (IsAdmin)
            {
                items = UoW.DevelopmentRepository.All();
            }
            else
            {
                items = UoW.DevelopmentRepository.GetByUserId(CurrentUser.Id);
            }
            

            List<DevelopmentAPIViewModel> itemsDefault = new List<DevelopmentAPIViewModel>();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            count = items.Count();
            itemsDefault.AddRange(items.ToList().Select(Convert));

            return new KendoResponse<IEnumerable<DevelopmentAPIViewModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<DevelopmentAPIViewModel> Post(DevelopmentAPIViewModel model)
        {
            DevelopmentAPIViewModel response;

            if (ModelState.IsValid)
            {                         
                Development item = new Development();
                item.Enabled = false;
                item.Client = GetHash(clientLength);
                item.Secret = GetHash(secretLength);

                Bind(item, model);
                UoW.DevelopmentRepository.Create(item);

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<DevelopmentAPIViewModel>
            {
                Response = response
            };
        }

        public override KendoResponse<DevelopmentAPIViewModel> Put(DevelopmentAPIViewModel model)
        {
            DevelopmentAPIViewModel response = new DevelopmentAPIViewModel();

            if (model.ID.HasValue)
            {
                Development item = UoW.DevelopmentRepository.GetFull(model.ID.Value);
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (IsAdmin)
                        {
                            item.Enabled = model.Enabled;
                        }
                        item.Name = model.Name;

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

            return new KendoResponse<DevelopmentAPIViewModel>
            {
                Response = response
            };
        }

        protected override Development Bind(Development item, DevelopmentAPIViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            item.Name = model.Name;
            item.UserID = CurrentUser.Id;

            return item;
        }

        protected override DevelopmentAPIViewModel Convert(Development item)
        {
            if (item == null)
            {
                return null;
            }

            return new DevelopmentAPIViewModel
            {
                ID = item.Id,
                Name = item.Name,
                Client = item.Client,
                Secret = item.Secret,
                Enabled = item.Enabled
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