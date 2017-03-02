/**
 * @file IApplicationRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IApplicationRepository : IBaseRepository<Application>
    {
        IEnumerable<Application> All(int? take = null, int? skip = null);

        Application GetByClient(string client);

        Application GetByClientAndSecret(string client, string secret);

        IEnumerable<Application> GetByUserId(int userId, int? take = null, int? skip = null);
    }
}
