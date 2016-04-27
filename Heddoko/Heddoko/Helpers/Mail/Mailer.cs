using DAL.Models;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Heddoko
{
    public class Mailer
    {
        public async static Task SendActivationEmail(User user)
        {
            ActivationUserEmailViewModel mailModel = new ActivationUserEmailViewModel();
            mailModel.Token = user.ConfirmToken;

            string subject = i18n.Resources.EmailActivationUserSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ActivationUserEmail", mailModel));

            Mail.Send(subject, body, user.Email);
        }

        public async static Task SendForgotPasswordEmail(User user)
        {
            ForgotPasswordEmailViewModel mailModel = new ForgotPasswordEmailViewModel();
            mailModel.ForgotToken = user.ForgotToken;
            mailModel.FirstName = user.FirstName;
            mailModel.LastName = user.LastName;

            string subject = i18n.Resources.EmailForgotPasswordSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ForgotPasswordEmail", mailModel));

            Mail.Send(subject, body, user.Email);
        }

        public async static Task SendForgotUsernameEmail(User user)
        {
            ForgotPasswordEmailViewModel mailModel = new ForgotPasswordEmailViewModel();
            mailModel.Username = user.Username;
            mailModel.FirstName = user.FirstName;
            mailModel.LastName = user.LastName;

            string subject = i18n.Resources.EmailForgotUsernameSubject;

            string body = await Task.Run(() => RazorView.RenderViewToString("ForgotUsernameEmail", mailModel));

            Mail.Send(subject, body, user.Email);
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

            Mail.Send(subject, renderedBody, email);

            return renderedBody;
        }
    }
}