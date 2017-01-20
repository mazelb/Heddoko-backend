/**
 * @file SendGridMail.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using DAL;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Services.MailSending
{
    public static class SendGridMail
    {
        private const string TypeHtml = "text/html";
        private const string Disposition = "attachment";

        public static void Send(string subject,
                                string body,
                                string mailTo = "",
                                IEnumerable<HttpPostedFileBase> attachments = null,
                                string mailFrom = null,
                                bool enableTracking = true)
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
                    mail.TrackingSettings = new TrackingSettings
                    {
                        ClickTracking = new ClickTracking()
                        {
                            Enable = true
                        },
                        OpenTracking = new OpenTracking()
                        {
                            Enable = true
                        }
                    };
                }

                dynamic response = sendGrid.client.mail.send.post(requestBody: mail.Get()).GetAwaiter().GetResult();
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