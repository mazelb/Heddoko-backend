﻿/**
 * @file RecordRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using DAL.Models.Enum;
using DAL.Repository.Interface;

namespace DAL.Repository
{
    public class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(HDContext sb) : base(sb)
        {
        }

        public override Record Get(int id)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.Assets)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Record> GetRecordByOrganization(int organizationID, int teamID, int take, int? skip, int? userID)
        {
            IQueryable<Record> query = DbSet.Include(c => c.User)
                                            .Include(c => c.Assets)
                                            .Where(c => c.Type == RecordType.Record)
                                            .Where(c => c.User.OrganizationID == organizationID)
                                            .Where(c => c.User.TeamID == teamID)
                                            .OrderByDescending(c => c.Created);

            if (userID.HasValue)
            {
                query = query.Where(c => c.UserID == userID);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);

            return query;
        }

        public int GetRecordByOrganizationCount(int organizationID, int teamID, int? userID = null)
        {
            IQueryable<Record> query = DbSet.Where(c => c.Type == RecordType.Record)
                                            .Where(c => c.User.OrganizationID == organizationID)
                                            .Where(c => c.User.TeamID == teamID);

            if (userID.HasValue)
            {
                query = query.Where(c => c.UserID == userID);
            }

            return query.Count();
        }

        public IEnumerable<Record> GetRecordsByTeam(int teamId, int take, int? skip = 0)
        {
            IQueryable<Record> query = DbSet.Include(c => c.User)
                                            .Include(c => c.Assets)
                                            .Where(c => c.Type == RecordType.Record)
                                            .Where(c => c.User.TeamID == teamId)
                                            .OrderByDescending(c => c.Created);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);

            return query;
        }

        public int GetRecordsByTeamCount(int teamId)
        {
            return DbSet.Count(c => c.Type == RecordType.Record && c.User.TeamID == teamId);
        }

        public IEnumerable<Record> GetDefaultRecords(int take, int? skip = 0)
        {
            IQueryable<Record> query = DbSet.Include(c => c.User)
                                            .Include(c => c.Assets)
                                            .Where(c => c.Type == RecordType.DefaultRecord)
                                            .OrderByDescending(c => c.Created);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);

            return query;
        }

        public int GetDefaultRecordsCount()
        {
            return DbSet.Count(c => c.Type == RecordType.DefaultRecord);
        }
    }
}
