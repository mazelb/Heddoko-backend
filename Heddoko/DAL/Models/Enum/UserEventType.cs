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
