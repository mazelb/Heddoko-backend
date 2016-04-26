using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace Heddoko
{
    public class MailFile
    {
        public Stream Attachment;
        public string Name { get; set; }
    }

    public class Mail
    {
        private const string HtmlContentID = "htmlView";
        public static void Send(string subject, string body, string mailTo = "", IEnumerable<MailFile> attachments = null)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();
            foreach (string mail in mailTo.Split(','))
            {
                message.To.Add(new MailAddress(mail?.Trim()));
            }

            message.Subject = subject;
            message.IsBodyHtml = true;


            AlternateView alternativeView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            alternativeView.ContentId = HtmlContentID;
            alternativeView.TransferEncoding = TransferEncoding.SevenBit;

            //alternativeView.LinkedResources.Add(AddResource(@"Content\img\logo.jpg", "logo"));

            message.AlternateViews.Add(alternativeView);

            if (attachments != null)
            {
                foreach (MailFile file in attachments)
                {
                    message.Attachments.Add(new Attachment(file.Attachment, file.Name));
                }
            }
            client.Send(message);
        }

        private static LinkedResource AddResource(string filename, string contentId)
        {
            string path = Path.Combine(Config.ApplicationPath, filename);

            ContentType c = new ContentType("image/jpeg");

            LinkedResource linkedResource = new LinkedResource(path);
            linkedResource.ContentType = c;
            linkedResource.ContentId = contentId;
            linkedResource.TransferEncoding = TransferEncoding.Base64;

            return linkedResource;
        }
    }
}