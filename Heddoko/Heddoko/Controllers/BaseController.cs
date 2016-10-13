using System.Web.Http.Description;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Services;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UnitOfWork _uow;
        private User _currentUser;

        public BaseController()
        {
        }

        public BaseController(ApplicationUserManager userManager, UnitOfWork uow)
        {
            UserManager = userManager;
            UoW = uow;
        }

        public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, UnitOfWork uow)
            : this(userManager, uow)
        {
            SignInManager = signInManager;
        }



        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        protected UnitOfWork UoW
        {
            get
            {
                return _uow ?? HttpContext.GetOwinContext().Get<UnitOfWork>();
            }
            private set
            {
                _uow = value;
            }
        }

        protected User CurrentUser => _currentUser ?? (_currentUser = UserManager.FindByIdCached(User.Identity.GetUserId<int>()));

        protected bool IsAuth => CurrentUser != null;

    }
}