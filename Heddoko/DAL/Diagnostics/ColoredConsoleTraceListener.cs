using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Diagnostics
{
    public class ColoredConsoleTraceListener : ConsoleTraceListener
    {
        public override void TraceEvent(TraceEventCache eventCache, string source,
           TraceEventType eventType, int id, string message)
        {
            Console.ForegroundColor = GetColor(eventType);
            WriteLine(string.Format("{0}: {1}", eventType, message));
        }

        ConsoleColor GetColor(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Error:
                case TraceEventType.Critical: return ConsoleColor.Red; ;
                case TraceEventType.Warning: return ConsoleColor.Yellow;
                case TraceEventType.Information: return ConsoleColor.White;
                default: return ConsoleColor.Green;
            }
        }


        public override void TraceEvent(TraceEventCache eventCache, string source,
            TraceEventType eventType, int id, string format, params object[] args)
        {
            Console.ForegroundColor = GetColor(eventType);
            if (args != null)
            {
                WriteLine(string.Format("{0}: {1}", eventType, string.Format(format, args)));
            }
            else
            {
                WriteLine(string.Format("{0}: {1}", format));
            }
        }
    }
}
