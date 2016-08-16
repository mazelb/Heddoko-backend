using System;

namespace DAL.Models
{
    [Flags]
    public enum ShirtQAStatusType : long
    {
        None = 0,
        TestedAndReady = 1,
        Fail = 2,
        BaseplateInspectionPass = 4,
        BaseplateInspectionFail = 8,
        WiringInspectionPass = 16,
        WiringInspectionFail = 32,
        ConnectorInspectionPass = 64,
        ConnectorInspectionFail = 128,
        HeatShrinkInspectionPass = 256,
        HeatShrinkInspectionFail = 512,
        PowerInspectionPass = 1024,
        PowerInspectionFail = 2048,
        SeamsInspectionPass = 4096,
        SeamsInspectionFail = 8192,
        IDLabelInspectionPass = 16384,
        IDLabelInspectionFail = 32768
    }
}
