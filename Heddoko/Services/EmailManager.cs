using DAL;
using DAL.Models;
using Hangfire;
using Services.MailSending;
using Services.MailSending.Models;

namespace Services
{
    public class EmailManager
    {
        [Queue(Constants.HangFireQueue.MailSending)]
        public static void SendActivationEmail(User user)
        {
            Mailer.SendActivationEmail(user);
        }

        [Queue(Constants.HangFireQueue.MailSending)]
        public static void SendInviteAdminEmail(Organization organization)
        {
            Mailer.SendInviteAdminEmail(organization);
        }

        [Queue(Constants.HangFireQueue.MailSending)]
        public static void SendInviteEmail(User user)
        {
            Mailer.SendInviteEmail(user);
        }

        [Queue(Constants.HangFireQueue.MailSending)]
        public static void SendForgotPasswordEmail(User user)
        {
            Mailer.SendForgotPasswordEmail(user);
        }

        [Queue(Constants.HangFireQueue.MailSending)]
        public static void SendForgotUsernameEmail(User user)
        {
            Mailer.SendForgotUsernameEmail(user);
        }

        [Queue(Constants.HangFireQueue.MailSending)]
        public static void SendSupportEmail(SupportEmailViewModel model)
        {
            Mailer.SendSupportEmail(model);
        }

        [Queue(Constants.HangFireQueue.MailSending)]
        public static void SendActivatedEmail(User user)
        {
            Mailer.SendActivatedEmail(user);
        }
    }
}
