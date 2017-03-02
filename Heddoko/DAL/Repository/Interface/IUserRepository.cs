/**
 * @file IUserRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IUserRepository : IBaseRepository<User>
    {
        void SetCache(User user);

        void ClearCache(User user);

        User GetByEmailCached(string email);

        User GetByUsernameCached(string username);

        User GetByUsername(string username);

        User GetByUsernameFull(string username);

        User GetByEmail(string email);

        IEnumerable<User> Admins();

        IEnumerable<User> All(bool isDeleted = false);

        IEnumerable<User> GetByOrganization(int value, bool isDeleted = false, int? licenseID = null);

        IEnumerable<int> GetIdsByOrganization(int organizationID, bool isDeleted = false);

        IEnumerable<User> GetByTeam(int value, bool isDeleted = false);

        IEnumerable<int> GetIdsByTeam(int teamID, bool isDeleted = false);

        IEnumerable<User> Search(string search, int? organizationID = null, bool isDeleted = false, int? licenseID = null);

        IEnumerable<User> GetByOrganizationAPI(int organizationID, int teamID, int take, int? skip = 0);

        int GetByOrganizationAPICount(int organizationID, int teamID);
    }
}