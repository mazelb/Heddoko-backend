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
        Worker = 4
    }
}
