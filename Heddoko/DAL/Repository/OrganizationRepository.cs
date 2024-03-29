﻿/**
 * @file OrganizationRepository.cs
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
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(HDContext sb)
            : base(sb)
        {
        }

        public IEnumerable<Organization> All(bool isDeleted = false)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .Where(c => isDeleted ? c.Status == OrganizationStatusType.Deleted : c.Status != OrganizationStatusType.Deleted)
                        .OrderBy(c => c.Name);
        }

        public Organization GetByName(string name)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();
        }

        public override Organization GetFull(int id)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Organization> Search(string value, bool isDeleted = false)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .Where(c => isDeleted ? c.Status == OrganizationStatusType.Deleted : c.Status != OrganizationStatusType.Deleted)
                        .Where(c => c.Id.ToString().ToLower().Contains(value.ToLower())
                                    || c.Name.ToLower().Contains(value.ToLower())
                                    || c.Address.ToLower().Contains(value.ToLower())
                                    || c.Phone.ToLower().Contains(value.ToLower()));
        }

        public IEnumerable<Organization> GetAllAPI(int take, int? skip = 0)
        {
            IQueryable<Organization> query = DbSet.Where(c => c.Status != OrganizationStatusType.Deleted)
                                                  .OrderBy(c => c.Name);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);

            return query;
        }

        public int GetAllAPICount()
        {
            return DbSet.Count(c => c.Status != OrganizationStatusType.Deleted);
        }
    }
}