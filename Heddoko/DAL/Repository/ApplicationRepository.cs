/**
 * @file ApplicationRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(HDContext sb)
            : base(sb)
        {
        }

        public IEnumerable<Application> All(int? take = null, int? skip = null)
        {
            IQueryable<Application> query = DbSet.OrderByDescending(c => c.Created);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }
        public override Application GetFull(int id)
        {
            return DbSet.FirstOrDefault(c => c.Id == id);
        }

        public Application GetByClient(string client)
        {
            return DbSet.FirstOrDefault(c => c.Client == client);
        }

        public Application GetByClientAndSecret(string client, string secret)
        {
            return DbSet.FirstOrDefault(c => c.Client == client && c.Secret == secret);
        }

        public IEnumerable<Application> GetByUserId(int userId, int? take = null, int? skip = null)
        {
            IQueryable<Application> query = DbSet.Where(c => c.UserID == userId)
                        .OrderByDescending(c => c.Created);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }
    }
}