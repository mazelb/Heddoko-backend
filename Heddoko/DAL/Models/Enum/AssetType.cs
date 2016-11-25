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
        Firmware = 4,
        [StringValue(Constants.Assets.Log)]
        Log = 5,
        [StringValue(Constants.Assets.SystemLog)]
        SystemLog = 6,
        [StringValue(Constants.Assets.Setting)]
        Setting = 7,
        [StringValue(Constants.Assets.Record)]
        Record = 8,
        [StringValue(Constants.Assets.DefaultRecords)]
        DefaultRecords = 9,
        [StringValue(Constants.Assets.Guide)]
        Guide = 10,
        [StringValue(Constants.Assets.ProcessedFrameData)]
        ProcessedFrameData = 11,
        [StringValue(Constants.Assets.AnalysisFrameData)]
        AnalysisFrameData = 12,
        [StringValue(Constants.Assets.RawFrameData)]
        RawFrameData = 13
    }
}
