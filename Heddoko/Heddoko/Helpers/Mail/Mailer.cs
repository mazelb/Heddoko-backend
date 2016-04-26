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