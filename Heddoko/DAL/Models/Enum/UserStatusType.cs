/**
 * @file UserStatusType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Models
{
    public enum UserStatusType
    {
        NotActive = 0,
        Active = 1,
        Banned = 2,
        Deleted = 3,
        Invited = 4,
        Pending = 5
    }
}
