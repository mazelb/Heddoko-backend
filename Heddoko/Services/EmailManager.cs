using DAL;
using DAL.Models;
using Hangfire;
using Services.MailSending;
using Services.MailSending.Models;

namespace Services
{
    public class EmailManager
    {
        [Queue(Constants.HangFireQueue.Email)]
        public static void SendActivationEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            var user = uow.UserRepository.Get(userId);

            Mailer.SendActivationEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendInviteAdminEmail(int organizationId)
        {
            UnitOfWork uow = new UnitOfWork();
            var organization = uow.OrganizationRepository.Get(organizationId);

            Mailer.SendInviteAdminEmail(organization);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendInviteEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            var user = uow.UserRepository.Get(userId);

            Mailer.SendInviteEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendForgotPasswordEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            var user = uow.UserRepository.Get(userId);

            Mailer.SendForgotPasswordEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendForgotUsernameEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            var user = uow.UserRepository.Get(userId);

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
            var user = uow.UserRepository.Get(userId);

            Mailer.SendActivatedEmail(user);
        }
    }
}
