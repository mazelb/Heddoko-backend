﻿namespace DAL.Models
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
