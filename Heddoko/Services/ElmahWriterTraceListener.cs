/**
 * @file ElmahWriterTraceListener.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using Elmah;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class ElmahListener : System.Diagnostics.TraceListener
    {
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            TraceEvent(eventCache, source, eventType, id, string.Format(format, args));
        }
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message) //
        {
            Exception exception;
            switch (eventType)
            {
                case TraceEventType.Information:
                    exception = new TraceInformation(message);
                    break;
                case TraceEventType.Error:
                    exception = new TraceError(message);
                    break;
                case TraceEventType.Warning:
                    exception = new TraceWarning(message);
                    break;
                default:
                    exception = new TraceWrite(message);
                    break;
            }
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                ErrorLog.GetDefault(null).Log(new Error(exception));
            }
            else
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            base.TraceTransfer(eventCache, source, id, message, relatedActivityId);
        }
        public override void Write(string message)
        {
        }
        public override void WriteLine(string message)
        {
        }
    }
}
