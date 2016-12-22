using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace Heddoko.Helpers.DomainRouting
{
    public class DomainRouteConstraint : IRouteConstraint, IHttpRouteConstraint
    {
        private readonly string[] _domains;

        public DomainRouteConstraint(params string[] domains)
        {
            if (domains == null)
            {
                throw new ArgumentNullException(nameof(domains));
            }

            _domains = domains;
        }

        private bool Match(Uri url)
        {
            return url != null && _domains.Contains(url.Host.ToLowerInvariant());
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            return Match(request.RequestUri);
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Match(httpContext.Request.Url);
        }
    }
}