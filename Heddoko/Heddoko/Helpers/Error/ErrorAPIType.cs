using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko
{
    public enum ErrorAPIType
    {
        Info,
        Warning,
        Error,
        EmailOrPassword,
        SigninValidation,
        UserIsBanned,
        UserIsNotActive,
        UserIsExist,
        WrongEmailForgotPassword,
        OldPasswordWrongMessage,
        UserNotFound,
        WrongObjectAccess,
        ObjectNotFound,
        ObjectWasDeleted,
        LicenseIsNotReady
    }
}