﻿using System;
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
            OrganizationStatusType status = isDeleted ? OrganizationStatusType.Deleted : OrganizationStatusType.Active;

            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .Where(c => c.Status == status)
                        .OrderBy(c => c.Name);
        }

        public Organization GetByName(string name)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault(c => c.Status == OrganizationStatusType.Active);
        }

        public override Organization GetFull(int id)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Organization> Search(string value, bool isDeleted = false)
        {
            OrganizationStatusType status = isDeleted ? OrganizationStatusType.Deleted : OrganizationStatusType.Active;

            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .Where(c => c.Status == status)
                        .Where(c => c.ID.ToString().ToLower().Contains(value.ToLower())
                                    || c.Name.ToLower().Contains(value.ToLower())
                                    || c.Address.ToLower().Contains(value.ToLower())
                                    || c.Phone.ToLower().Contains(value.ToLower()));
        }
    }
}