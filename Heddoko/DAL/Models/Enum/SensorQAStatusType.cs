namespace DAL.Models
{
    public enum SensorQAStatusType
    {
        None = 0,
        TestedAndReady = 1,
        FirmawareUpdated = 2,
        SeatingInBase = 4,
        Led = 8,
        Orientation = 16,
        Drif = 32
    }
}
