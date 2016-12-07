using System.Web.Http;
using DAL;
using Heddoko.Helpers.DomainRouting.Http;

namespace Heddoko.Controllers.PublicAPI
{
    [Authorize]
    [RoutePrefix("openApi")]
    public class TestAPIController : ApiController
    {
        [DomainRoute("test", Constants.ConfigKeyName.PublicApiSite)]
        [HttpPost]
        public IHttpActionResult Test()
        {
            return null;
        }
        
        [DomainRoute("index", Constants.ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public IHttpActionResult Index()
        {
            return null;
        }
    }
}
