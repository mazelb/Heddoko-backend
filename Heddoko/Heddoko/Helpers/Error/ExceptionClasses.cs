using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heddoko
{
    internal class TraceInformation : Exception
    {
        internal TraceInformation(string message) : base(message) { }
    }

    internal class TraceError : Exception
    {
        internal TraceError(string message) : base(message) { }
    }

    internal class TraceWarning : Exception
    {
        internal TraceWarning(string message) : base(message) { }
    }
    internal class TraceWrite : Exception
    {
        internal TraceWrite(string message) : base(message) { }
    }
}
