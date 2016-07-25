using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Heddoko.Models;
using Newtonsoft.Json;

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
            Trace.TraceError($"ExceptionAPIHandler.{guid}: Unhandled exception caught in API '{context.Request.RequestUri}'  Body: '{context.Request.Content.ReadAsStringAsync().Result}' Error: {context.Exception}");

            context.Result = new ErrorResult
            {
                Guid = guid,
                Request = context.Request,
                Errors = ErrorMessage.Get(context.Exception, guid)
            };
        }

        private class ErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { private get; set; }

            public Guid Guid { private get; set; }

            public List<ErrorAPIViewModel> Errors { private get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        Errors = new
                        {
                            Guid,
                            Errors,
                            Method = Request.Method.ToString(),
                            ID = Request.GetRouteData().Values.FirstOrDefault(c => c.Key == "id").Value
                        }
                    })),
                    RequestMessage = Request
                };


                return Task.FromResult(response);
            }
        }
    }
}