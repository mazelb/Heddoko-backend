/**
 * @file IAssetRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        Asset GetByImage(string name);

        IEnumerable<Asset> GetRecordByOrganization(int organizationID, int teamID, int take, int? skip = 0, int? userID = null);

        int GetRecordByOrganizationCount(int organizationID, int teamID, int? userID = null);
    }
}