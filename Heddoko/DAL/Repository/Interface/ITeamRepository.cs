/**
 * @file ITeamRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        IEnumerable<Team> GetByOrganization(int organizationID, bool isDeleted = false);

        IEnumerable<Team> All(int? organizationID = null, bool isDeleted = false);

        IEnumerable<Team> Search(string search, int? organizationID = null, bool isDeleted = false);

        IEnumerable<Team> GetByOrganizationAPI(int organizationId, int take, int? skip = 0);

        int GetByOrganizationCount(int organizationId);
    }
}