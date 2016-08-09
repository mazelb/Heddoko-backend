namespace DAL.Models
{
    public enum SensorQAStatusType
    {
        TestedAndReady = 0,
        Fail = 1,
        FirmawareUpdatedPass = 2,
        FirmawareUpdatedFail = 3,
        SeatingInBasePass = 4,
        SeatingInBaseFail = 5,
        LedPass = 6,
        LedFail = 7,
        OrientationPass = 8,
        OrientationFail = 9,
        DrifPass = 10,
        DrifFail = 11
    }
}
