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

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [AuthAPI(Roles = Constants.Roles.Admin)]
    public abstract class BaseAdminController<T, TM> : ApiController
        where T : IBaseModel
        where TM : class
    {
        protected BaseAdminController()
        {
            UoW = new UnitOfWork();
            Mapper.Initialize(cfg =>
                              {
                                  cfg.CreateMap<T, TM>();
                              });

            CurrentUser = Forms.ValidateSession(UoW);
            if (CurrentUser == null)
            {
                ThrowAccessException();
            }
        }

        protected UnitOfWork UoW { get; }

        protected User CurrentUser { get; private set; }

        [Route("")]
        [HttpGet]
        public virtual KendoResponse<IEnumerable<TM>> Get([FromUri] KendoRequest request)
        {
            throw new NotSupportedException();
        }

        [Route("{id:int}")]
        [HttpGet]
        public virtual KendoResponse<TM> Get(int id)
        {
            throw new NotSupportedException();
        }

        [Route("history/{id:int}")]
        [HttpGet]
        public virtual KendoResponse<IEnumerable<HistoryNotes>> History(int id)
        {
            throw new NotSupportedException();
        }

        [Route("")]
        [HttpPost]
        public virtual KendoResponse<TM> Post(TM model)
        {
            throw new NotSupportedException();
        }

        [Route("bulk")]
        [HttpPatch]
        public virtual KendoResponse<IEnumerable<TM>> Bulk(KendoRequest model)
        {
            throw new NotSupportedException();
        }

        [Route("{id:int?}")]
        [HttpPut]
        public virtual KendoResponse<TM> Put(TM model)
        {
            throw new NotSupportedException();
        }

        [Route("{id:int}")]
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

        protected void InvalidateCurrentUser()
        {
            CurrentUser = Forms.ValidateSession(UoW, true);
        }

        protected void ThrowAccessException()
        {
            throw new APIException(ErrorAPIType.Info, Resources.WrongObjectAccess);
        }
    }
}