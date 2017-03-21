/**
 * @file TeamRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Team GetFull(int id)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Users)
                        .Include(c => c.Users.Select(u => u.Devices))
                        .Include(c => c.Licenses)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Team> All(int? organizationID = null, bool isDeleted = false)
        {
            TeamStatusType status = isDeleted ? TeamStatusType.Deleted : TeamStatusType.Active;

            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Licenses)
                        .Where(c => c.Status == status)
                        .Where(c => !organizationID.HasValue || c.OrganizationID == organizationID)
                        .OrderBy(c => c.Name);
        }

        public IEnumerable<Team> Search(string search, int? organizationID = null, bool isDeleted = false)
        {
            TeamStatusType status = isDeleted ? TeamStatusType.Deleted : TeamStatusType.Active;

            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Licenses)
                        .Where(c => c.Status == status)
                        .Where(c => !organizationID.HasValue || c.OrganizationID == organizationID)
                        .Where(c => c.Id.ToString().ToLower().Contains(search.ToLower())
                                    || c.Name.ToLower().Contains(search.ToLower())
                                    || c.Address.ToLower().Contains(search.ToLower()));
        }

        public IEnumerable<Team> GetByOrganization(int organizationID, bool isDeleted = false)
        {
            TeamStatusType status = isDeleted ? TeamStatusType.Deleted : TeamStatusType.Active;

            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Licenses)
                        .Where(c => c.Status == status)
                        .Where(c => c.OrganizationID == organizationID)
                        .OrderBy(c => c.Name);
        }

        public IEnumerable<Team> GetByOrganizationAPI(int organizationId, int take, int? skip = 0)
        {
            IQueryable<Team> query = DbSet.Where(c => c.Status != TeamStatusType.Deleted)
                                          .Where(c => c.OrganizationID == organizationId)
                                          .OrderBy(c => c.Name);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);

            return query;
        }

        public int GetByOrganizationCount(int organizationId)
        {
            return DbSet.Count(c => c.Status != TeamStatusType.Deleted &&
                                    c.OrganizationID == organizationId);
        }
    }
}