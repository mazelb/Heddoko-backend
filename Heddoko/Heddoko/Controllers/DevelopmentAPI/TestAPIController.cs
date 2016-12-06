using System.Web.Mvc;
using DAL;
using Heddoko.Controllers.API;

namespace Heddoko.Controllers.DevelopmentAPI
{
    public class TestAPIController : Controller
    {
        public string Test(string subdomain)
        {
            return subdomain;
        }
    }
}
