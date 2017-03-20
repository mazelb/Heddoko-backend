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
                SendGridClient client = new SendGridClient(Config.SendgridKey);

                EmailAddress from = new EmailAddress(mailFrom ?? Config.MailFrom);
                EmailAddress to = new EmailAddress(mailTo);

                var mail = new SendGridMessage()
                {
                    From = from,
                    Subject = subject,
                    HtmlContent = body
                };

                mail.AddTo(to);

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

                            mail.AddAttachment(attachment.FileName, Convert.ToBase64String(buffer), attachment.ContentType, Disposition);
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

                Response response = client.SendEmailAsync(mail).GetAwaiter().GetResult();

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