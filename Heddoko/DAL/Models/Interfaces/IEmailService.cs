/**
 * @file IEmailService.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public interface IEmailService
    {
        void SendActivationEmail(int userId, string code);

        void SendInviteAdminEmail(int organizationId, string inviteToken);

        void SendInviteEmail(int userId, string inviteToken);

        void SendPasswordEmail(int userId, string password);

        void SendForgotPasswordEmail(int userId, string resetPasswordToken);

        void SendForgotUsernameEmail(int userId);

        void SendSupportEmail(ISupportEmailViewModel model);

        void SendActivatedEmail(int userId);
    }
}
