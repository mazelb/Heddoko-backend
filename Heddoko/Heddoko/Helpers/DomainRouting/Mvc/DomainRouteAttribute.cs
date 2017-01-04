using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace Heddoko.Helpers.DomainRouting.Mvc
{
    public class DomainRouteAttribute : RouteFactoryAttribute
    {
        private readonly string _domainConfigNames;

        public DomainRouteAttribute(string template, string domainConfigNames) : base(template)
        {
            _domainConfigNames = domainConfigNames;
        }

        public override RouteValueDictionary Constraints
        {
            get
            {
                string[] domains = _domainConfigNames.Split(',')
                                                     .Select(siteConfigName => UrlHelper.GetHost(ConfigurationManager.AppSettings[siteConfigName]))
                                                     .ToArray();

                var constraints = new RouteValueDictionary { { "domain", new DomainRouteConstraint(domains) } };

                return constraints;
            }
        }
    }
}