using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class SubdomainRoute : RouteBase
{
    private readonly string _subDomain;
    private readonly RouteValueDictionary _routeData;

    public SubdomainRoute(string subDomain, object routeData) :
        this(subDomain, new RouteValueDictionary(routeData))
    { }

    public SubdomainRoute(string subDomain, RouteValueDictionary routeData)
    {
        _subDomain = subDomain;
        _routeData = routeData;
    }

    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
        var url = httpContext.Request.Headers["HOST"];

        var index = url.IndexOf(".", StringComparison.Ordinal);
        if (index < 0)
        {
            return null;
        }

        var firstDomain = url.Split('.')[0];
        if (!firstDomain.Equals(_subDomain))
        {
            return null;
        }

        var handler = new MvcRouteHandler();
        var result = new RouteData { RouteHandler = handler };
        foreach (var route in _routeData)
        {
            result.Values.Add(route.Key, route.Value);
        }

        return result;
    }

    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
    {
        //Implement your formating Url formating here
        return null;
    }
}