namespace Heddoko
{
    //sync with Heddoko.DAL
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
        LicenseIsNotReady,
        KitID,
        AssetType,
        UserIsNotApproved
    }
}