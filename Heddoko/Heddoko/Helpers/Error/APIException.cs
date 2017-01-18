/**
 * @file APIException.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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