﻿namespace DAL.Models
{
    public enum ShirtOctopiQAStatusType
    {
        None = 0,
        BaseplateInspection = 1,
        WiringInspection = 2,
        ConnectorInspection = 4,
        HeatShrinkInspection = 8,
        PowerInspection = 16,
        SeamsInspection = 32,
        IDLabelInspection = 64,
        TestedAndReady = BaseplateInspection | WiringInspection | ConnectorInspection | HeatShrinkInspection
                        | PowerInspection | SeamsInspection | IDLabelInspection
    }
}
