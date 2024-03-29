﻿/**
 * @file EmailService.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmailService : IIdentityMessageService, IDisposable
    {
        public EmailService(IEmailService service)
        {
            Service = service;
        }

        public IEmailService Service { get; set; }

        public Task SendAsync(IdentityMessage message)
        {
            //Todo add simple send
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            Service = null;
        }
    }
}
