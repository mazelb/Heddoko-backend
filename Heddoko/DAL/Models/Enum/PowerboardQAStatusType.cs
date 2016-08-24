namespace DAL.Models
{
    public enum PowerboardQAStatusType
    {
        None = 0,
        PowerboardProgrammed = 1,
        PowerboardUSBEnum = 2,
        BatteryInstalled = 4,
        TestedAndReady = PowerboardProgrammed | PowerboardUSBEnum | BatteryInstalled
    }
}
