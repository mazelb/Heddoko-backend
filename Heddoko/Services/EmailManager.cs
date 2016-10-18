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
        public static void SendInviteAdminEmail(int organizationId, string inviteToken)
        {
            UnitOfWork uow = new UnitOfWork();
            Organization organization = uow.OrganizationRepository.GetFull(organizationId);

            Mailer.SendInviteAdminEmail(organization, inviteToken);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendInviteEmail(int userId, string inviteToken)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetFull(userId);

            Mailer.SendInviteEmail(user, inviteToken);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendForgotPasswordEmail(int userId, string resetPasswordToken)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendForgotPasswordEmail(user,  resetPasswordToken);
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
