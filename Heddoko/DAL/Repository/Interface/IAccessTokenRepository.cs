/**
 * @file IAccessTokenRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;

namespace DAL
{
    public interface IAccessTokenRepository : IBaseRepository<AccessToken>
    {
        AccessToken GetByToken(string token);
    }
}