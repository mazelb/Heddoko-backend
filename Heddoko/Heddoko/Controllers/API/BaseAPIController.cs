using System.Web.Http;
using DAL;
using DAL.Models;
using Services;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Heddoko.Controllers.API
{
    public class BaseAPIController : ApiController
    {
        private ApplicationUserManager _userManager;
        private UnitOfWork _uow;
        private User _currentUser;

        public BaseAPIController()
        {
        }

        public BaseAPIController(ApplicationUserManager userManager, UnitOfWork uow)
        {
            UserManager = userManager;
            UoW = uow;
        }

        public bool IsAuth => CurrentUser != null;

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

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
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
    }
}