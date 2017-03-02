/**
 * @file IAssemblyCacheRepositoy.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Models;

namespace DAL
{
    public interface IAssemblyCacheRepository : ICacheRepository<List<Assembly>>
    {
        List<Assembly> GetCached();
        void ClearCache();

        void SetCache(List<Assembly> item);
    }
}
