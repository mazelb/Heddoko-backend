﻿using DAL.Models;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAL;

namespace Heddoko
{
    public class Mailer
    {
        public const string WrikeEmail = "wrike@wrike.com";
        public async static Task SendActivationEmail(User user)
        {
            ActivationUserEmailViewModel mailModel = new ActivationUserEmailViewModel();
            mailModel.Token = user.ConfirmToken;

            string subject = i18n.Resources.EmailActivationUserSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ActivationUserEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public async static Task SendInviteAdminEmail(Organization organization)
        {
            InviteAdminUserEmailViewModel mailModel = new InviteAdminUserEmailViewModel();
            mailModel.Token = organization.User.InviteToken;
            mailModel.FirstName = organization.User.Name;
            mailModel.OrganizationName = organization.Name;

            string subject = i18n.Resources.EmailInviteAdminUserSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("InviteAdminUserEmail", mailModel));

            SendGridMail.Send(subject, body, organization.User.Email);
        }

        public async static Task SendInviteEmail(User user)
        {
            InviteAdminUserEmailViewModel mailModel = new InviteAdminUserEmailViewModel();
            mailModel.Token = user.InviteToken;
            mailModel.FirstName = user.Name;
            mailModel.OrganizationName = user.Organization.Name;

            string subject = i18n.Resources.EmailInviteUserSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("InviteUserEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public async static Task SendForgotPasswordEmail(User user)
        {
            ForgotPasswordEmailViewModel mailModel = new ForgotPasswordEmailViewModel();
            mailModel.ForgotToken = user.ForgotToken;
            mailModel.FirstName = user.FirstName;
            mailModel.LastName = user.LastName;

            string subject = i18n.Resources.EmailForgotPasswordSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ForgotPasswordEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public async static Task SendForgotUsernameEmail(User user)
        {
            ForgotPasswordEmailViewModel mailModel = new ForgotPasswordEmailViewModel();
            mailModel.Username = user.Username;
            mailModel.FirstName = user.FirstName;
            mailModel.LastName = user.LastName;

            string subject = i18n.Resources.EmailForgotUsernameSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ForgotUsernameEmail", mailModel));

            SendGridMail.Send(subject, body, user.Email);
        }

        public static void SendSupportEmail(SupportIndexViewModel model)
        {
            string env = Config.Environment == Constants.Environments.Prod ? string.Empty : Config.Environment;
            string subject = $"{model.Type.GetStringValue()} {env} {model.ShortDescription}";

            string from = string.Empty;
            switch (model.Type)
            {
                case IssueType.NewFeature:
                    from = "features@heddoko.com";
                    break;
                default:
                    from = "support@heddoko.com";
                    break;
            }

            SupportEmailViewModel mailModel = new SupportEmailViewModel()
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

        public async static Task SendActivatedEmail(User user)
        {
            await SendEmail(user.Email, i18n.Resources.EmailActivatedSubject, i18n.Resources.EmailActivatedBody);
        }

        public async static Task<string> SendEmail(string email, string subject, string body)
        {
            EmailViewModel mailModel = new EmailViewModel();
            mailModel.Body = body;
            mailModel.Email = email;

            string renderedBody = await Task.Run(() => RazorView.RenderViewToString("Email", mailModel));

            SendGridMail.Send(subject, renderedBody, email);

            return renderedBody;
        }
    }
}