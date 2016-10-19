using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TraceInformation : Exception
    {
        public TraceInformation(string message) : base(message) { }
    }

    public class TraceError : Exception
    {
        public TraceError(string message) : base(message) { }
    }

    public class TraceWarning : Exception
    {
        public TraceWarning(string message) : base(message) { }
    }
    public class TraceWrite : Exception
    {
        public TraceWrite(string message) : base(message) { }
    }
}
