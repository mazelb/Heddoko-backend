/**
 * @file IComponentRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IComponentRepository : IBaseRepository<Component>
    {
        IEnumerable<Component> Search(string value, bool isDeleted);
        IEnumerable<Component> All(bool isDeleted);
        IEnumerable<Component> GetAvailable(int? id = null);
        int GetQuantityReadyOfComponent(ComponentsType type);
    }
}