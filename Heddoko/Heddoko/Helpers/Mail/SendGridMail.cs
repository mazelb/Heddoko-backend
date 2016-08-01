﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Diagnostics;

namespace Heddoko
{
    public class SendGridMail
    {
        private const string TypeHtml = "text/html";
        private const string Disposition = "attachment";

        public static void Send(string subject, string body, string mailTo = "", IEnumerable<HttpPostedFileBase> attachments = null, string mailFrom = null, bool enableTracking = true)
        {
            try
            {
                SendGridAPIClient sendGrid = new SendGridAPIClient(Config.SendgridKey);

                Email from = new Email(mailFrom ?? Config.MailFrom);
                Email to = new Email(mailTo);

                Content content = new Content(TypeHtml, body);

                SendGrid.Helpers.Mail.Mail mail = new SendGrid.Helpers.Mail.Mail(from, subject, to, content);

                if (attachments != null)
                {
                    foreach (HttpPostedFileBase attachment in attachments)
                    {
                        if (attachment != null)
                        {
                            byte[] buffer = new byte[attachment.ContentLength];
                            using (BinaryReader theReader = new BinaryReader(attachment.InputStream))
                            {
                                buffer = theReader.ReadBytes(attachment.ContentLength);
                            }

                            mail.AddAttachment(new Attachment()
                            {
                                Content = Convert.ToBase64String(buffer),
                                Filename = attachment.FileName,
                                Type = attachment.ContentType,
                                Disposition = Disposition
                            });

                            buffer = null;
                        }
                    }
                }

                if (enableTracking)
                {
                    mail.TrackingSettings = new TrackingSettings();

                    mail.TrackingSettings.ClickTracking = new ClickTracking()
                    {
                        Enable = true
                    };

                    mail.TrackingSettings.OpenTracking = new OpenTracking()
                    {
                        Enable = true
                    };
                }

                dynamic response = sendGrid.client.mail.send.post(requestBody: mail.Get());
                string result = response.Body.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(result))
                {
                    Trace.TraceError($"SendGridMail.Send Email:{mailTo} Body:{body} Result:{result}");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"SendGridMail.Send.Exception Email:{mailTo} Body:{body} Exception:{ex.GetBaseException()}");
            }
        }
    }
}