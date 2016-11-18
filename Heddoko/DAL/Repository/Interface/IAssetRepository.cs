﻿using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        Asset GetByImage(string name);

        IEnumerable<Asset> GetRecordByOrganization(int organizationID, int teamID, int take, int? skip = 0, int? userID = null);

        int GetRecordByOrganizationCount(int organizationID, int teamID, int? userID = null);

        IEnumerable<Asset> GetRecordsByTeam(int teamId, int take, int? skip = 0);

        int GetRecordsByTeamCount(int teamId);
    }
}