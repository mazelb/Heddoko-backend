using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Heddoko
{
    public class SendGridMail
    {
        private const string TypeHtml = "text/html";

        public static void Send(string subject, string body, string mailTo = "", IEnumerable<MailFile> attachments = null)
        {
            SendGridAPIClient sendGrid = new SendGridAPIClient(Config.SendgridKey);

            Email from = new Email(Config.MailFrom);
            Email to = new Email(mailTo);

            Content content = new Content(TypeHtml, body);

            SendGrid.Helpers.Mail.Mail mail = new SendGrid.Helpers.Mail.Mail(from, subject, to, content);

            dynamic response = sendGrid.client.mail.send.post(requestBody: mail.Get());
        }
    }
}