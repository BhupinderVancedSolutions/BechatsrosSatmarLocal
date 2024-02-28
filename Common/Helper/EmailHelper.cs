

using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;
using System;

namespace Common.Helper
{
    public class EmailHelper
    {
        public async static Task<bool> SendMailWithAttachment(string from, string to, string sendGridKey, string subject, string body, byte[] attachment, string attachmentName, string fromName = "", string replyToEmail = "")
        {

            bool status = false;
            //try
            //{
            var client = new SendGridClient(sendGridKey);
            var messageEmail = new SendGridMessage()
            {
                From = new EmailAddress(from),
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
                messageEmail.SetFrom(from, fromName);
            }
            messageEmail.AddTo(new EmailAddress(to));
            var file = Convert.ToBase64String(attachment);
            messageEmail.AddAttachment(attachmentName, file);
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
        }


    }
}
