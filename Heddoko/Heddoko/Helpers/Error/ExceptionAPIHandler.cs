using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Heddoko.Helpers.Error
{
    public class ExceptionAPIHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            Stream reqStream = context.Request.Content.ReadAsStreamAsync().Result;

            if (reqStream.CanSeek)
            {
                reqStream.Position = 0;
            }

            Guid guid = Guid.NewGuid();
            Trace.TraceError("ExceptionAPIHandler.{0}: Unhandled exception caught in API '{1}'  Body: '{2}' Error: {3}",
                guid,
                context.Request.RequestUri,
                context.Request.Content.ReadAsStringAsync().Result,
                context.Exception.ToString());

            context.Result = new ErrorResult()
            {
                Guid = guid,
                Request = context.Request,
                Errors = ErrorMessage.Get(context.Exception, guid)
            };
        }

        private class ErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public Guid Guid { get; set; }

            public List<ErrorAPIViewModel> Errors { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Errors = new
                    {
                        Guid = Guid,
                        Errors = Errors,
                        Method = Request.Method.ToString(),
                        ID = Request.GetRouteData().Values.FirstOrDefault(c => c.Key == "id").Value
                    }
                }));

                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}