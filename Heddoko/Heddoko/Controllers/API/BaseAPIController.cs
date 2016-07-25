using System.Web.Http;
using DAL;
using DAL.Models;

namespace Heddoko.Controllers.API
{
    public class BaseAPIController : ApiController
    {
        public BaseAPIController()
        {
            UoW = new UnitOfWork();
        }

        protected UnitOfWork UoW { get; private set; }

        public bool IsAuth => CurrentUser != null;

        private User _currentUser;

        protected User CurrentUser
        {
            get
            {
                if (!ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    return null;
                }

                return _currentUser ?? (_currentUser = UoW.UserRepository.GetIDCached(int.Parse(ControllerContext.RequestContext.Principal.Identity.Name)));
            }
        }
    }
}