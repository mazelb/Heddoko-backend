using System.Web.Http;
using DAL;
using Heddoko.Helpers.DomainRouting.Http;
using Heddoko.Helpers.Auth;
using static DAL.Constants;

namespace Heddoko.Controllers.PublicAPI
{
    [ClaimsAuthorization(ClaimType = OpenAPIClaims.ClaimType, ClaimValue = OpenAPIClaims.ClaimValue)]
    [RoutePrefix("openApi")]
    public class TestAPIController : ApiController
    {
        [DomainRoute("test", ConfigKeyName.PublicApiSite)]
        [HttpPost]
        public IHttpActionResult Test()
        {
            return null;
        }
        
        [DomainRoute("index", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public IHttpActionResult Index()
        {
            return null;
        }
    }
}
