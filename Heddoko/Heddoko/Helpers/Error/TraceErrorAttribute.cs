using System.Diagnostics;
using System.Web.Mvc;

namespace Heddoko
{
    public class TraceErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext ctx)
        {
            Trace.TraceError("TraceErrorAttribute: Unhandled exception caught in controller '{1}' action '{2}': {0}",
                ctx.Exception,
                (string)ctx.RouteData.Values["controller"],
                (string)ctx.RouteData.Values["action"]);
        }
    }
}