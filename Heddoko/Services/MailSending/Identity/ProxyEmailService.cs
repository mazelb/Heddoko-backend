using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL;
using DAL.Models;
using Hangfire;
using Services.MailSending.Models;

namespace Services.MailSending
{
    public class ProxyEmailService : IEmailService
    {
        public static EmailService Create()
        {
            return new EmailService(new ProxyEmailService());
        }

        public void SendActivationEmail(int userId, string code)
        {
            BackgroundJob.Enqueue(() => EmailManager.SendActivationEmail(userId, code));
        }

        public void SendInviteAdminEmail(int organizationId)
        {
            BackgroundJob.Enqueue(() => EmailManager.SendInviteAdminEmail(organizationId));
        }

        public void SendInviteEmail(int userId)
        {
            BackgroundJob.Enqueue(() => EmailManager.SendInviteEmail(userId));
        }

        public void SendForgotPasswordEmail(int userId)
        {
            BackgroundJob.Enqueue(() => EmailManager.SendForgotPasswordEmail(userId));
        }

        public void SendForgotUsernameEmail(int userId)
        {
            BackgroundJob.Enqueue(() => EmailManager.SendForgotUsernameEmail(userId));
        }

        public void SendSupportEmail(ISupportEmailViewModel model)
        {
            BackgroundJob.Enqueue(() => EmailManager.SendSupportEmail(model));
        }

        public void SendActivatedEmail(int userId)
        {
            BackgroundJob.Enqueue(() => EmailManager.SendActivatedEmail(userId));
        }
    }
}
