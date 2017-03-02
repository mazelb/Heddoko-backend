/**
 * @file UserStore.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserStore : UserStore<User, IdentityRole, int,
       UserLogin, UserRole, UserClaim>
    {
        private UserRepository Repository { get; set; }
        public UserStore(HDContext context)
            : base(context)
        {
            Repository = new UserRepository(context);
        }

        public User FindByIdCached(int userId)
        {
            return Repository.GetIDCached(userId);
        }

        public User FindByNameCached(string userName)
        {
            return Repository.GetByUsernameCached(userName);
        }

        public User FindByEmailCached(string email)
        {
            return Repository.GetByEmailCached(email);
        }

        public User FindByToken(string token)
        {
            return Repository.GetByToken(token);
        }
    }
}