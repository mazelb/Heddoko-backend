using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Heddoko.Models;

namespace Heddoko
{
    public static class RazorView
    {
        public static string RenderViewToString(string viewName, object model, string controllerName = "Email")
        {
            EmailViewModel emailModel = model as EmailViewModel;

            string result = null;
            using (StringWriter writer = new StringWriter())
            {
                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", controllerName);
                ControllerContext fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))),
                    routeData,
                    new FakeController());
                RazorViewEngine razorViewEngine = new RazorViewEngine();
                ViewEngineResult razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);
                ViewDataDictionary viewData = new ViewDataDictionary
                {
                    Model = model
                };
                ViewContext viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, viewData, new TempDataDictionary(), writer);
                razorViewResult.View.Render(viewContext, writer);
                result = writer.ToString();
            }

            return result;
        }

        private class FakeController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        }
    }
}