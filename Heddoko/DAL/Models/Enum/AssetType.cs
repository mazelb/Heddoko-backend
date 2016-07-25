namespace DAL.Models
{
    public enum AssetType
    {
        [StringValue(Constants.Assets.Seed)]
        Seed = 0,
        [StringValue(Constants.Assets.User)]
        User = 1,
        [StringValue(Constants.Assets.Profile)]
        Profile = 2,
        [StringValue(Constants.Assets.Group)]
        Group = 3,
        [StringValue(Constants.Assets.Firmware)]
        Firmware = 4
    }
}
