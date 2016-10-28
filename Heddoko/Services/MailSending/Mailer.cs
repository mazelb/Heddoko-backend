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
                ActivationUrl = $"{Config.DashboardSite}/confirm/{user.Id}/{HttpUtility.UrlEncode(code)}"
            };

            string subject = Resources.EmailActivationUserSubject;

            string body = razorView.RenderViewToString("ActivationUserEmail", mailModel);

            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendInviteAdminEmail(Organization organization, string inviteToken)
        {
            var mailModel = new InviteAdminUserEmailViewModel
            {
                Token = inviteToken,
                FirstName = organization.User.Name,
                OrganizationName = organization.Name,
                ActivationUrl = $"{Config.DashboardSite}/invite/{organization.User.Id}/{HttpUtility.UrlEncode(inviteToken)}"
            };

            string subject = Resources.EmailInviteAdminUserSubject;

            string body = razorView.RenderViewToString("InviteAdminUserEmail", mailModel);

            SendGridMail.Send(subject, body, organization.User.Email);
        }

        public static void SendInviteEmail(User user, string inviteToken)
        {
            var mailModel = new InviteAdminUserEmailViewModel
            {
                Token = inviteToken,
                FirstName = user.Name,
                OrganizationName = user.Organization?.Name,
                ActivationUrl = $"{Config.DashboardSite}/invite/{user.Id}/{HttpUtility.UrlEncode(inviteToken)}"
            };

            string subject = Resources.EmailInviteUserSubject;

            string body = razorView.RenderViewToString("InviteUserEmail", mailModel);

            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendForgotPasswordEmail(User user, string resetPasswordToken)
        {
            var mailModel = new ForgotPasswordEmailViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ResetPasswordUrl = $"{Config.DashboardSite}/forgot/{user.Id}/{HttpUtility.UrlEncode(resetPasswordToken)}"
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

        public static void SendLicenseExpiringEmail(License license)
        {
            var mailModel = new LicenseExpiringEmailViewModel
            {
                Name = license.Organization.User.Name,
                Days = (license.ExpirationAt.Date - DateTime.Now.Date).Days,
                LicenseName = license.Name,
                OrganizationName = license.Organization.Name,
                ExpirationAt = license.ExpirationAt,
                Email = license.Organization.User.Email
            };

            string subject = Resources.LicenseIsExpiring;

            string body = razorView.RenderViewToString("LicenseExpiringEmail", mailModel);

            SendGridMail.Send(subject, body, license.Organization.User.Email);
        }

        public static void SendLicenseExpiringAdminEmail(User admin, License license)
        {
            var mailModel = new LicenseExpiringEmailViewModel
            {
                Name = admin.Name,
                Days = (license.ExpirationAt.Date - DateTime.Now.Date).Days,
                LicenseName = license.Name,
                OrganizationName = license.Organization.Name,
                ExpirationAt = license.ExpirationAt,
                Email = admin.Email
            };

            string subject = Resources.LicenseIsExpiring;

            string body = razorView.RenderViewToString("LicenseExpiringAdminEmail", mailModel);

            SendGridMail.Send(subject, body, admin.Email);
        }
    }
}