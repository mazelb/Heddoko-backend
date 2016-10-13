using System.Web;
using DAL;
using DAL.Models;
using Hangfire;
using Services.MailSending;
using Services.MailSending.Models;

namespace Services
{
    public static class EmailManager
    {
        [Queue(Constants.HangFireQueue.Email)]
        public static void SendActivationEmail(int userId, string code)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendActivationEmail(user, code);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendInviteAdminEmail(int organizationId)
        {
            UnitOfWork uow = new UnitOfWork();
            Organization organization = uow.OrganizationRepository.GetIDCached(organizationId);

            Mailer.SendInviteAdminEmail(organization);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendInviteEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendInviteEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendForgotPasswordEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendForgotPasswordEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendForgotUsernameEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendForgotUsernameEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendSupportEmail(ISupportEmailViewModel model)
        {
            Mailer.SendSupportEmail(model);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendActivatedEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendActivatedEmail(user);
        }
    }
}
