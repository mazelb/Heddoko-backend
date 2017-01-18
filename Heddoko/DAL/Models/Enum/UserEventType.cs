/**
 * @file UserEventType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Models.Enum
{
    public enum UserEventType
    {
        StreamChannelOpened,
        StreamChannelClosed,
        LicenseAddedToOrganization,
        LicenseRemovedFromOrganization,
        LicenseAddedToUser,
        LicenseRemovedFromUser,
        LicenseChangedForUser,
        LicenseExpiring,
        LicenseExpired
    }
}
