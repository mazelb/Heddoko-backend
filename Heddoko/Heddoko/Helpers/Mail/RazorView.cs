using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Heddoko
{
    public class RazorView
    {
        class FakeController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        }

        public static string RenderViewToString(string viewName, object model, string controllerName = "Email")
        {
            EmailViewModel emailModel = model as EmailViewModel;
            
            string result = null;
            using (var writer = new StringWriter())
            {
                var routeData = new RouteData();
                routeData.Values.Add("controller", controllerName);
                var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
                var razorViewEngine = new RazorViewEngine();
                var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);
                ViewDataDictionary viewData = new ViewDataDictionary();
                viewData.Model = model;
                var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, viewData, new TempDataDictionary(), writer);
                razorViewResult.View.Render(viewContext, writer);
                result = writer.ToString();
            }

            return result;
        }
    }
}
