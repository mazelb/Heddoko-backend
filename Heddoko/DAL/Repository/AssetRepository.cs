﻿/**
 * @file AssetRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(HDContext sb) : base(sb)
        {
        }

        public Asset GetByImage(string name)
        {
            return DbSet.FirstOrDefault(c => c.Image.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Asset> GetRecordByOrganization(int organizationID, int teamID, int take, int? skip = 0, int? userID = null)
        {
            IQueryable<Asset> query = DbSet.Include(c => c.User)
                                           .Where(c => c.Type == AssetType.Record)
                                           .Where(c => c.Status == UploadStatusType.Uploaded)
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
            IQueryable<Asset> query = DbSet.Where(c => c.Type == AssetType.Record)
                                           .Where(c => c.Status == UploadStatusType.Uploaded)
                                           .Where(c => c.User.OrganizationID == organizationID)
                                           .Where(c => c.User.TeamID == teamID);

            if (userID.HasValue)
            {
                query = query.Where(c => c.UserID == userID);
            }

            return query.Count();
        }
    }
}
