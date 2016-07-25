using System;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;

namespace Heddoko
{
    public static class Mailer
    {
        private const string WrikeEmail = "wrike@wrike.com";

        public static async Task SendActivationEmail(User user)
        {
            ActivationUserEmailViewModel mailModel = new ActivationUserEmailViewModel
            {
                Token = user.ConfirmToken
            };

            string subject = Resources.EmailActivationUserSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ActivationUserEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public static async Task SendInviteAdminEmail(Organization organization)
        {
            InviteAdminUserEmailViewModel mailModel = new InviteAdminUserEmailViewModel
            {
                Token = organization.User.InviteToken,
                FirstName = organization.User.Name,
                OrganizationName = organization.Name
            };

            string subject = Resources.EmailInviteAdminUserSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("InviteAdminUserEmail", mailModel));

            SendGridMail.Send(subject, body, organization.User.Email);
        }

        public static async Task SendInviteEmail(User user)
        {
            InviteAdminUserEmailViewModel mailModel = new InviteAdminUserEmailViewModel
            {
                Token = user.InviteToken,
                FirstName = user.Name,
                OrganizationName = user.Organization.Name
            };

            string subject = Resources.EmailInviteUserSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("InviteUserEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public static async Task SendForgotPasswordEmail(User user)
        {
            ForgotPasswordEmailViewModel mailModel = new ForgotPasswordEmailViewModel
            {
                ForgotToken = user.ForgotToken,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            string subject = Resources.EmailForgotPasswordSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ForgotPasswordEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public static async Task SendForgotUsernameEmail(User user)
        {
            ForgotPasswordEmailViewModel mailModel = new ForgotPasswordEmailViewModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            string subject = Resources.EmailForgotUsernameSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ForgotUsernameEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendSupportEmail(SupportIndexViewModel model)
        {
            string env = DAL.Config.Environment == Constants.Environments.Prod ? string.Empty : DAL.Config.Environment;
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

            SupportEmailViewModel mailModel = new SupportEmailViewModel
            {
                Type = model.Type,
                Importance = model.Importance,
                Entered = DateTime.Now,
                DetailedDescription = model.DetailedDescription,
                Email = model.Email,
                FullName = model.FullName,
                ShortDescription = model.ShortDescription
            };

            string body = RazorView.RenderViewToString("SupportEmail", mailModel);

            SendGridMail.Send(subject, body, WrikeEmail, model.Attachments, from, false);
        }

        public static async Task SendActivatedEmail(User user)
        {
            await SendEmail(user.Email, Resources.EmailActivatedSubject, Resources.EmailActivatedBody);
        }

        private static async Task<string> SendEmail(string email, string subject, string body)
        {
            EmailViewModel mailModel = new EmailViewModel
            {
                Body = body,
                Email = email
            };

            string renderedBody = await Task.Run(() => RazorView.RenderViewToString("Email", mailModel));

            SendGridMail.Send(subject, renderedBody, email);

            return renderedBody;
        }
    }
}