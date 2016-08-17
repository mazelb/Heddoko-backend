namespace DAL.Models
{
    public enum PantsOctopiQAStatusType
    {
        None = 0,
        TestedAndReady = 1,
        BaseplateInspection = 2,
        WiringInspection = 4,
        ConnectorInspection = 8,
        HeatShrinkInspection = 16,
        PowerInspection = 32,
        SeamsInspection = 64,
        IDLabelInspection = 128
    }
}
