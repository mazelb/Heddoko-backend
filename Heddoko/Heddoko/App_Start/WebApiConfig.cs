using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Heddoko.Helpers.Error;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Heddoko
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            config.Routes.MapHttpRoute(
                "AdminDefaultApi",
                "admin/api/{controller}/{id}",
                new
                {
                    id = RouteParameter.Optional
                }
                );

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new
                {
                    id = RouteParameter.Optional
                }
                );

            JsonMediaTypeFormatter json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Services.Replace(typeof(IExceptionHandler), new ExceptionAPIHandler());
        }
    }
}