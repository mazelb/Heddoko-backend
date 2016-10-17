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

        void SendForgotPasswordEmail(int userId, string resetPasswordToken);

        void SendForgotUsernameEmail(int userId);

        void SendSupportEmail(ISupportEmailViewModel model);

        void SendActivatedEmail(int userId);
    }
}
