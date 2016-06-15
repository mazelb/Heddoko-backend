﻿using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace DAL
{
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(HDContext sb) : base(sb)
        {
        }

        public override IEnumerable<Organization> All()
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Licenses)
                        .OrderBy(c => c.Name);
        }

        public Organization GetByName(string name)
        {
            return All().Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public Organization GetFull(int id)
        {
            return All().Where(c => c.ID == id).FirstOrDefault();
        }

        public IEnumerable<Organization> Search(string value)
        {
            return All().Where(c => c.ID.ToString().ToLower().Contains(value.ToLower())
                                 || c.Name.ToLower().Contains(value.ToLower())
                                 || c.Address.ToLower().Contains(value.ToLower())
                                 || c.Phone.ToLower().Contains(value.ToLower()));
        }
    }
}
