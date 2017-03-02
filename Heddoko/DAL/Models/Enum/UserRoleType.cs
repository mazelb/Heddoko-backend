/**
 * @file UserRoleType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Models
{
    public enum UserRoleType
    {
        [StringValue(Constants.Roles.Admin)]
        Admin = 0,
        [StringValue(Constants.Roles.Analyst)]
        Analyst = 1,
        [StringValue(Constants.Roles.User)]
        User = 2,
        [StringValue(Constants.Roles.LicenseAdmin)]
        LicenseAdmin = 3,
        [StringValue(Constants.Roles.Worker)]
        Worker = 4,
        [StringValue(Constants.Roles.ServiceAdmin)]
        ServiceAdmin = 5,
        [StringValue(Constants.Roles.LicenseUniversal)]
        LicenseUniversal = 6
    }
}
