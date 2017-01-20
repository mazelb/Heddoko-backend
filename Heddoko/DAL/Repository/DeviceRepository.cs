/**
 * @file DeviceRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Linq;
using DAL.Models;
using DAL.Repository.Interface;
using EntityFramework.Extensions;

namespace DAL.Repository
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(HDContext context) : base(context)
        {
        }

        public Device GetByToken(string token)
        {
            return DbSet.FirstOrDefault(c => c.Token.Equals(token, StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteByToken(string token)
        {
            DbSet.Where(c => c.Token.Equals(token, StringComparison.OrdinalIgnoreCase)).Delete();
        }
    }
}
