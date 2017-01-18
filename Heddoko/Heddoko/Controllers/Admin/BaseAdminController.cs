/**
 * @file BaseAdminController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using i18n;
using Services;
using System.Web;
using Heddoko.Helpers.DomainRouting.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public abstract class BaseAdminController<T, TM> : ApiController
        where T : class
        where TM : class
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UnitOfWork _uow;
        private User _currentUser;

        protected BaseAdminController() : base()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<T, TM>();
            });
        }

        protected BaseAdminController(ApplicationUserManager userManager, UnitOfWork uow) : this()
        {
            UserManager = userManager;
            UoW = uow;
        }

        protected BaseAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, UnitOfWork uow)
            : this(userManager, uow)
        {
            SignInManager = signInManager;
        }



        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected UnitOfWork UoW
        {
            get
            {
                return _uow ?? HttpContext.Current.GetOwinContext().Get<UnitOfWork>();
            }
            private set
            {
                _uow = value;
            }
        }

        protected User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = UserManager.FindByIdCached(User.Identity.GetUserId<int>());
                }
                return _currentUser;
            }
        }

        protected bool IsAdmin => CurrentUser != null && UserManager.IsInRole(CurrentUser.Id, DAL.Constants.Roles.Admin);

        protected bool IsLicenseAdmin => CurrentUser != null && UserManager.IsInRole(CurrentUser.Id, DAL.Constants.Roles.LicenseAdmin);


        [DomainRoute("", DAL.Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public virtual KendoResponse<IEnumerable<TM>> Get([FromUri] KendoRequest request)
        {
            throw new NotSupportedException();
        }

        [DomainRoute("{id:int}", DAL.Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public virtual KendoResponse<TM> Get(int id)
        {
            throw new NotSupportedException();
        }

        [DomainRoute("history/{id:int}", DAL.Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public virtual KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            throw new NotSupportedException();
        }

        [DomainRoute("", DAL.Constants.ConfigKeyName.DashboardSite)]
        [HttpPost]
        public virtual KendoResponse<TM> Post(TM model)
        {
            throw new NotSupportedException();
        }

        [DomainRoute("bulk", DAL.Constants.ConfigKeyName.DashboardSite)]
        [HttpPatch]
        public virtual KendoResponse<IEnumerable<TM>> Bulk(KendoRequest model)
        {
            throw new NotSupportedException();
        }

        [DomainRoute("{id:int?}", DAL.Constants.ConfigKeyName.DashboardSite)]
        [HttpPut]
        public virtual KendoResponse<TM> Put(TM model)
        {
            throw new NotSupportedException();
        }

        [DomainRoute("{id:int}", DAL.Constants.ConfigKeyName.DashboardSite)]
        [HttpDelete]
        public virtual KendoResponse<TM> Delete(int id)
        {
            throw new NotSupportedException();
        }

        protected virtual T Bind(T item, TM model)
        {
            throw new NotSupportedException();
        }

        protected virtual TM Convert(T item)
        {
            return Mapper.Map<TM>(item);
        }

        protected void ThrowAccessException()
        {
            throw new APIException(ErrorAPIType.Info, Resources.WrongObjectAccess);
        }
    }
}