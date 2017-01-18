/**
 * @file IOrganizationRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IOrganizationRepository : IBaseRepository<Organization>
    {
        Organization GetByName(string name);

        IEnumerable<Organization> All(bool isDeleted = false);

        IEnumerable<Organization> Search(string search, bool isDeleted = false);

        IEnumerable<Organization> GetAllAPI(int take, int? skip = 0);

        int GetAllAPICount();
    }
}