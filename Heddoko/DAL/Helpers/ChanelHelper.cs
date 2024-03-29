﻿/**
 * @file ChanelHelper.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;

namespace DAL.Helpers
{
    public static class ChanelHelper
    {
        public static string GetChannelName(User user)
        {
            return $"team-{user.TeamID}-{user.Kit?.Id}";
        }
    }
}
