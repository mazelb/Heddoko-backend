namespace DAL.Models
{
    public enum PantsOctopiQAStatusType
    {
        TestedAndReady = 0,
        Fail = 1,
        BaseplateInspectionPass = 2,
        BaseplateInspectionFail = 3,
        WiringInspectionPass = 4,
        WiringInspectionFail = 5,
        ConnectorInspectionPass = 6,
        ConnectorInspectionFail = 7,
        HeatShrinkInspectionPass = 8,
        HeatShrinkInspectionFail = 9,
        PowerInspectionPass = 10,
        PowerInspectionFail = 11,
        SeamsInspectionPass = 12,
        SeamsInspectionFail = 13,
        IDLabelInspectionPass = 14,
        IDLabelInspectionFail = 15
    }
}
