using DTO.Request;
using DTO.Response;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IEmailTemplateService
    {        
        public Task<bool> SendForgotPasswordMail(string name, string email, string passwordResetLink);
        Task<bool> SendTransactionMail(TransactionRequestDto cardKnoxDonationReceiptRequest, string transactionId, bool isTransactionSucceeded, string errorMessage);
    }
}
