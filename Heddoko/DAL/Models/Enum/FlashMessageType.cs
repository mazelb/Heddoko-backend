/**
 * @file FlashMessageType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Models
{
    public enum FlashMessageType
    {
        [StringValue("alert-success")]
        Success = 0,
        [StringValue("alert-info")]
        Info = 1,
        [StringValue("alert-warning")]
        Warning = 2,
        [StringValue("alert-danger")]
        Error = 3
    }
}
