using DTO.Request;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IEmailTemplateService
    {        
        public Task<bool> SendForgotPasswordMail(string name, string email, string passwordResetLink);
        Task<bool> SendTransactionMail(TransactionRequestDto transactionRequestDto, string transactionId, bool isTransactionSucceeded, string errorMessage);
    }
}
