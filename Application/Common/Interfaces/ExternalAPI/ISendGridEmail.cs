using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models.Request;

namespace Application.Common.Interfaces.ExternalAPI
{
    public interface ISendGridEmail
    {
        Task<bool> SendMail(string to, string subject, string body, string fromName = "", string replyToEmail = "");
        Task<bool> SendMailWithPlainText(string to, string subject, string body, string plaintext, string fromName = "");
        Task<bool> SendMailWithAttachment(string to, string subject, string body, byte[] attachment, string attachmentName, string fromName = "", string replyToEmail = "");
        Task<bool> SendMailWithMultipleAttachments(string to, string subject, string body, List<AttachmentRequest> attachments, string fromName = "", string replyToEmail = "");
    }
}
