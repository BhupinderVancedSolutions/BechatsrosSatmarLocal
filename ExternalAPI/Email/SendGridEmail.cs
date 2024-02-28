using Application.Common.Interfaces.ExternalAPI;
using Application.Common.Models.Request;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ExternalAPI.Email
{

    public class SendGridEmail : ISendGridEmail
    {
        private readonly MailSetting _mailSetting;

        #region Ctor
        public SendGridEmail(IOptions<MailSetting> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }

        #endregion
        #region Send Grid

        public async Task<bool> SendMail(string to, string subject, string body, string fromName = "", string replyToEmail = "")
        {
            bool status = false;
            if (_mailSetting.IsUsingSendGridKey == true)
            {
                //try
                //{
                    var client = new SendGridClient(_mailSetting.SendGridKey);

                var messageEmail = new SendGridMessage()
                {
                    //From = new EmailAddress(from),
                    Subject = subject,
                    PlainTextContent = "",
                    HtmlContent = body
                };
                if (!string.IsNullOrEmpty(replyToEmail))
                {
                    messageEmail.ReplyTo = new EmailAddress()
                    {
                        Email = replyToEmail,
                        Name = ""
                    };
                }
                //if (!string.IsNullOrEmpty(fromName))
                //{
                //    messageEmail.SetFrom(from, fromName);
                //}
                messageEmail.AddTo(new EmailAddress(to));
                var response = await client.SendEmailAsync(messageEmail);
                if (response.IsSuccessStatusCode)
                {
                    status = true;
                }
                //}
                //catch
                //{
                //    status = false;
                //}
                return status;

                //    var msg = MailHelper.CreateSingleEmail(new EmailAddress(_mailSetting.FromEmail), new EmailAddress(to), subject, "", body);
                //    if (!string.IsNullOrEmpty(fromName))
                //    {
                //        msg.SetFrom(_mailSetting.FromEmail, fromName);
                //    }
                //    if (!string.IsNullOrEmpty(replyToEmail))
                //    {
                //        msg.ReplyTo = new EmailAddress()
                //        {
                //            Email = replyToEmail,
                //            Name = ""
                //        };
                //    }
                //    var response = await client.SendEmailAsync(msg);
                //    if (response.IsSuccessStatusCode)
                //    {
                //        status = true;
                //    }
                //}
                //catch
                //{
                //    status = false;
                //}
                //return status;
            }
            else
            {
                try
                {
                    using (MailMessage mailMessage = new())
                    {
                        mailMessage.From = new MailAddress(_mailSetting.FromEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(to));
                        SmtpClient smtp = new()
                        {
                            Host = _mailSetting.SmtpHost,
                            EnableSsl = Convert.ToBoolean(_mailSetting.EnableSsl)
                        };
                        System.Net.NetworkCredential NetworkCred = new()
                        {
                            UserName = _mailSetting.UserName,
                            Password = _mailSetting.Password
                        };
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = _mailSetting.SmtpPort;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 60000;
                        smtp.Send(mailMessage);
                        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                        status = true;
                    }
                }
                catch
                {
                    status = false;
                }

            }
            return status;

        }

        public async Task<bool> SendMailWithPlainText(string to, string subject, string body, string plaintext, string fromName = "")
        {
            bool status = false;
            if (_mailSetting.IsUsingSendGridKey == true)
            {
                try
                {
                    var client = new SendGridClient(_mailSetting.SendGridKey);
                    var msg = MailHelper.CreateSingleEmail(new EmailAddress(_mailSetting.FromEmail), new EmailAddress(to), subject, plaintext, body);
                    if (!string.IsNullOrEmpty(fromName))
                    {
                        msg.SetFrom(_mailSetting.FromEmail, fromName);
                    }
                    var response = await client.SendEmailAsync(msg);
                    if (response.IsSuccessStatusCode)
                    {
                        status = true;
                    }
                }
                catch
                {
                    status = false;
                }
            }
            else
            {
                try
                {
                    using (MailMessage mailMessage = new())
                    {
                        mailMessage.From = new MailAddress(_mailSetting.FromEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false;
                        mailMessage.To.Add(new MailAddress(to));
                        SmtpClient smtp = new()
                        {
                            Host = _mailSetting.SmtpHost,
                            EnableSsl = Convert.ToBoolean(_mailSetting.EnableSsl)
                        };
                        System.Net.NetworkCredential NetworkCred = new()
                        {
                            UserName = _mailSetting.UserName,
                            Password = _mailSetting.Password
                        };
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = _mailSetting.SmtpPort;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 60000;
                        smtp.Send(mailMessage);
                        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                        status = true;
                    }
                }
                catch
                {
                    status = false;
                }
            }

            return status;
        }

        public async Task<bool> SendMailWithAttachment(string to, string subject, string body, byte[] attachment, string attachmentName, string fromName = "", string replyToEmail = "")
        {

            bool status = false;
            if (_mailSetting.IsUsingSendGridKey == true)
            {
                try
                {
                    var client = new SendGridClient(_mailSetting.SendGridKey);
                    var messageEmail = new SendGridMessage()
                    {
                        From = new EmailAddress(_mailSetting.FromEmail),
                        Subject = subject,
                        PlainTextContent = "",
                        HtmlContent = body
                    };
                    if (!string.IsNullOrEmpty(replyToEmail))
                    {
                        messageEmail.ReplyTo = new EmailAddress()
                        {
                            Email = replyToEmail,
                            Name = ""
                        };
                    }
                    if (!string.IsNullOrEmpty(fromName))
                    {
                        messageEmail.SetFrom(_mailSetting.FromEmail, fromName);
                    }
                    messageEmail.AddTo(new EmailAddress(to));
                    var file = Convert.ToBase64String(attachment);
                    messageEmail.AddAttachment(attachmentName, file);
                    var response = await client.SendEmailAsync(messageEmail);
                    if (response.IsSuccessStatusCode)
                    {
                        status = true;
                    }
                }
                catch
                {
                    status = false;
                }
            }
            else
            {
                try
                {
                    using (MailMessage mailMessage = new())
                    {
                        mailMessage.From = new MailAddress(_mailSetting.FromEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(to));
                        mailMessage.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(attachment), attachmentName));
                        SmtpClient smtp = new()
                        {
                            Host = _mailSetting.SmtpHost,
                            EnableSsl = Convert.ToBoolean(_mailSetting.EnableSsl)
                        };
                        System.Net.NetworkCredential NetworkCred = new()
                        {
                            UserName = _mailSetting.UserName,
                            Password = _mailSetting.Password
                        };
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = _mailSetting.SmtpPort;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 60000;
                        smtp.Send(mailMessage);
                        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                        status = true;
                    }
                }
                catch
                {
                    status = false;
                }
            }

            return status;
        }
        public async Task<bool> SendMailWithMultipleAttachments(string to, string subject, string body, List<AttachmentRequest> attachments, string fromName = "", string replyToEmail = "")
        {

            bool status = false;
            if (_mailSetting.IsUsingSendGridKey == true)
            {
                try
                {
                    var client = new SendGridClient(_mailSetting.SendGridKey);
                    var messageEmail = new SendGridMessage()
                    {
                        From = new EmailAddress(_mailSetting.FromEmail),
                        Subject = subject,
                        PlainTextContent = "",
                        HtmlContent = body
                    };
                    if (!string.IsNullOrEmpty(replyToEmail))
                    {
                        messageEmail.ReplyTo = new EmailAddress()
                        {
                            Email = replyToEmail,
                            Name = ""
                        };
                    }
                    if (!string.IsNullOrEmpty(fromName))
                    {
                        messageEmail.SetFrom(_mailSetting.FromEmail, fromName);
                    }
                    messageEmail.AddTo(new EmailAddress(to));
                    List<SendGrid.Helpers.Mail.Attachment> sendGridAttachments = new List<SendGrid.Helpers.Mail.Attachment>();
                    foreach (var attachment in attachments)
                    {
                        SendGrid.Helpers.Mail.Attachment sendGridAttachment = new SendGrid.Helpers.Mail.Attachment();
                        sendGridAttachment.Content = Convert.ToBase64String(attachment.Content);
                        sendGridAttachment.Filename = attachment.Filename;
                        sendGridAttachments.Add(sendGridAttachment);
                    }
                    messageEmail.AddAttachments(sendGridAttachments);
                    var response = await client.SendEmailAsync(messageEmail);
                    if (response.IsSuccessStatusCode)
                    {
                        status = true;
                    }
                }
                catch
                {
                    status = false;
                }
            }
            else
            {
                try
                {
                    using (MailMessage mailMessage = new())
                    {

                        mailMessage.From = new MailAddress(_mailSetting.FromEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress(to));
                        foreach (var item in attachments)
                        {
                            mailMessage.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(item.Content), item.Filename));
                        }
                        //mailMessage.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(attachment), attachmentName));
                        SmtpClient smtp = new()
                        {
                            Host = _mailSetting.SmtpHost,
                            EnableSsl = Convert.ToBoolean(_mailSetting.EnableSsl)
                        };
                        System.Net.NetworkCredential NetworkCred = new()
                        {
                            UserName = _mailSetting.UserName,
                            Password = _mailSetting.Password
                        };
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = _mailSetting.SmtpPort;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 60000;
                        smtp.Send(mailMessage);
                        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                        status = true;
                    }
                }
                catch
                {
                    status = false;
                }
            }

            return status;
        }

        #endregion
    }
}
