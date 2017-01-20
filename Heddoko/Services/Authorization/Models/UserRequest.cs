﻿/**
 * @file UserRequest.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace Services.Authorization.Models
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
