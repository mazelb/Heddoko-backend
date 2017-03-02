/**
 * @file IRecordRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repository.Interface
{
    public interface IRecordRepository : IBaseRepository<Record>
    {
        IEnumerable<Record> GetRecordByOrganization(int organizationID, int teamID, int take, int? skip, int? userID);

        int GetRecordByOrganizationCount(int organizationID, int teamID, int? userID);

        IEnumerable<Record> GetRecordsByTeam(int teamId, int take, int? skip = 0);

        int GetRecordsByTeamCount(int teamId);

        IEnumerable<Record> GetDefaultRecords(int take, int? skip = 0);

        int GetDefaultRecordsCount();
    }
}
