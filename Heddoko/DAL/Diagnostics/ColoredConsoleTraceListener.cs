using System;
using System.Diagnostics;

namespace DAL.Diagnostics
{
    public class ColoredConsoleTraceListener : ConsoleTraceListener
    {
        public override void TraceEvent(
            TraceEventCache eventCache,
            string source,
            TraceEventType eventType,
            int id,
            string message)
        {
            Console.ForegroundColor = GetColor(eventType);
            WriteLine($"{eventType}: {message}");
        }

        private ConsoleColor GetColor(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Error:
                case TraceEventType.Critical:
                    return ConsoleColor.Red;
                case TraceEventType.Warning:
                    return ConsoleColor.Yellow;
                case TraceEventType.Information:
                    return ConsoleColor.White;
                case TraceEventType.Verbose:
                case TraceEventType.Start:
                case TraceEventType.Stop:
                case TraceEventType.Suspend:
                case TraceEventType.Resume:
                case TraceEventType.Transfer:
                default:
                    return ConsoleColor.Green;
            }
        }


        public override void TraceEvent(
            TraceEventCache eventCache,
            string source,
            TraceEventType eventType,
            int id,
            string format,
            params object[] args)
        {
            Console.ForegroundColor = GetColor(eventType);
            WriteLine(args.Length > 0 ? $"{eventType}: {string.Format(format, args)}" : $"{format}");
        }
    }
}