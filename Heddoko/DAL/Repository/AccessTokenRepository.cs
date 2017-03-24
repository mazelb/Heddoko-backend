/**
 * @file AccessTokenRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class AccessTokenRepository : BaseRepository<AccessToken>, IAccessTokenRepository
    {
        public AccessTokenRepository(HDContext sb)
            : base(sb)
        {
        }

        public AccessToken GetByToken(string token)
        {
            return DbSet.Include(c => c.User)
                        .Include(c => c.User.Roles)
                        .FirstOrDefault(c => c.Token == token);
        }
    }
}