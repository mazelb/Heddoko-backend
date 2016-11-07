using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class SubdomainRoute : RouteBase
{

    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
        var host = httpContext.Request.Url.Host;
        var index = host.IndexOf(".");
        string[] segments = httpContext.Request.Url.PathAndQuery.Split('/');

        if (index < 0)
            return null;

        var subdomain = host.Substring(0, index);
        string controller = (segments.Length > 0) ? segments[0] : "Home";
        string action = (segments.Length > 1) ? segments[1] : "Index";

        var routeData = new RouteData(this, new MvcRouteHandler());
        routeData.Values.Add("controller", controller); 
        routeData.Values.Add("action", action); 
        routeData.Values.Add("subdomain", subdomain); 
        return routeData;
    }

    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
    {
        //Implement your formating Url formating here
        return null;
    }
}