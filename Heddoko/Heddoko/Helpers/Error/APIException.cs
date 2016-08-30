using System;

namespace Heddoko
{
    public class APIException : Exception
    {
        public APIException(ErrorAPIType type, string message)
            : base(message)
        {
            Code = (int) type;
        }

        public int Code { get; }
    }
}