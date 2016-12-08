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
