using DAL;
using System.Web.Http;

namespace Heddoko.Controllers.DevelopmentAPI
{
    [Authorize]
    public class TestAPIController : ApiController
    {
        [Route("test")]
        [HttpPost]
        public IHttpActionResult Test()
        {
            return null;
        }

        [Route("index")]
        public IHttpActionResult Index()
        {
            return null;
        }
    }
}
