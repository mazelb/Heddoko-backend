using System;
using System.Web;
using DAL;
using DAL.Models;
using i18n;
using Services.MailSending.Models;

namespace Services.MailSending
{
    public static class Mailer
    {
        private static readonly RazorView razorView;
        private const string EmailTemplatesFolder = "MailSending/Templates";
        private const string LayoutViewName = "_LayoutEmail";

        static Mailer()
        {
            razorView = new RazorView(EmailTemplatesFolder, LayoutViewName);
        }

        public static void SendActivationEmail(User user, string code)
        {
            var mailModel = new ActivationUserEmailViewModel
            {
                FirstName = user.FirstName,
                ActivationUrl = $"{Config.DashboardSite}/confirm/{user.Id}/{code}"
            };

            string subject = Resources.EmailActivationUserSubject;

            string body = razorView.RenderViewToString("ActivationUserEmail", mailModel);

            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendInviteAdminEmail(Organization organization)
        {
            var mailModel = new InviteAdminUserEmailViewModel
            {
                Token = organization.User.InviteToken,
                FirstName = organization.User.Name,
                OrganizationName = organization.Name
            };

            mailModel.ActivationUrl = $"{Config.DashboardSite}/invite/{mailModel.Token}";

            string subject = Resources.EmailInviteAdminUserSubject;

            string body = razorView.RenderViewToString("InviteAdminUserEmail", mailModel);

            SendGridMail.Send(subject, body, organization.User.Email);
        }

        public static void SendInviteEmail(User user)
        {
            var mailModel = new InviteAdminUserEmailViewModel
            {
                Token = user.InviteToken,
                FirstName = user.Name,
                OrganizationName = user.Organization?.Name
            };

            mailModel.ActivationUrl = $"{Config.DashboardSite}/invite/{mailModel.Token}";

            string subject = Resources.EmailInviteUserSubject;

            string body = razorView.RenderViewToString("InviteUserEmail", mailModel);

            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendForgotPasswordEmail(User user)
        {
            var mailModel = new ForgotPasswordEmailViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ResetPasswordUrl = $"{Config.DashboardSite}/forgot/{user.ForgotToken}"
            };

            string subject = Resources.EmailForgotPasswordSubject;

            string body = razorView.RenderViewToString("ForgotPasswordEmail", mailModel);


            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendForgotUsernameEmail(User user)
        {
            var mailModel = new ForgotPasswordEmailViewModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            string subject = Resources.EmailForgotUsernameSubject;

            string body = razorView.RenderViewToString("ForgotUsernameEmail", mailModel);

            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendSupportEmail(ISupportEmailViewModel model)
        {
            string env = Config.Environment == Constants.Environments.Prod ? string.Empty : Config.Environment;
            string subject = $"{model.Type.GetStringValue()} {env} {model.ShortDescription}";

            string from;
            switch (model.Type)
            {
                case IssueType.NewFeature:
                    from = "features@heddoko.com";
                    break;
                default:
                    from = "support@heddoko.com";
                    break;
            }

            var mailModel = new SupportEmailViewModel
            {
                Type = model.Type,
                Importance = model.Importance,
                Entered = DateTime.Now,
                DetailedDescription = model.DetailedDescription,
                Email = model.Email,
                FullName = model.FullName,
                ShortDescription = model.ShortDescription
            };

            string body = razorView.RenderViewToString("SupportEmail", mailModel);

            SendGridMail.Send(subject, body, Config.WrikeEmail, model.Attachments, from, false);
        }

        public static void SendActivatedEmail(User user)
        {
            var mailModel = new WelcomeEmailViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName
            };

            string subject = Resources.EmailActivatedSubject;
            
            string body = razorView.RenderViewToString("WelcomeUserEmail", mailModel);

            SendGridMail.Send(subject, body, user.Email);
        }
    }
}