/**
 * @file EquipmentStatusType.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Models
{
    public enum EquipmentStatusType
    {
        Ready = 0,
        InUse = 1,
        Defective = 2,
        InProduction = 3,
        Testing = 4,
        ForWash = 5,
        Trash = 6
    }
}
