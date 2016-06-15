using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public abstract class BaseAdminController<T, M> : ApiController
        where T : BaseModel
        where M : class
    {
        protected UnitOfWork UoW { get; set; }

        protected User CurrentUser { get; set; }

        public BaseAdminController()
        {
            UoW = new UnitOfWork();
            CurrentUser = Forms.ValidateSession(UoW);
            if (CurrentUser == null)
            {
                ThrowAccessException();
            }
        }

        [Route("")]
        [HttpGet]
        public virtual KendoResponse<IEnumerable<M>> Get([FromUri]KendoRequest request)
        {
            throw new NotSupportedException();
        }

        [Route("{id:int}")]
        [HttpGet]
        public virtual KendoResponse<M> Get(int id)
        {
            throw new NotSupportedException();
        }

        [Route("")]
        [HttpPost]
        public virtual KendoResponse<M> Post(M model)
        {
            throw new NotSupportedException();
        }

        [Route("bulk")]
        [HttpPatch]
        public virtual KendoResponse<IEnumerable<M>> Bulk(KendoRequest model)
        {
            throw new NotSupportedException();
        }

        [Route("{id:int?}")]
        [HttpPut]
        public virtual KendoResponse<M> Put(M model)
        {
            throw new NotSupportedException();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public virtual KendoResponse<M> Delete(int id)
        {
            throw new NotSupportedException();
        }

        protected virtual T Bind(T item, M model)
        {
            throw new NotSupportedException();
        }

        protected virtual M Convert(T item)
        {
            throw new NotSupportedException();
        }

        protected void InvalidateCurrentUser()
        {
            CurrentUser = Forms.ValidateSession(UoW, true);
        }

        protected void ThrowAccessException()
        {
            throw new APIException(ErrorAPIType.Info, i18n.Resources.WrongObjectAccess);
        }
    }
}
