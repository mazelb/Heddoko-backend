/**
 * @file PantsOctopiQAStatusType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace DAL.Models
{
    [Flags]
    public enum PantsOctopiQAStatusType : long
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
