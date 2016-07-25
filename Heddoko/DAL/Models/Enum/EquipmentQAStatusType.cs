namespace DAL.Models
{
    public enum EquipmentQAStatusType
    {
        FullTested = 0,
        BaseplateTested = 1,
        BaseplateFailed = 2,
        WiringTested = 3,
        WiringFailed = 4,
        ConnectorTested = 5,
        ConnectorFailed = 6,
        PowerTested = 7,
        PowerFailed = 7,
        Failed = 7
    }
}
