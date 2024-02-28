

using DTO.Request;
using DTO.Response;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services.HelperService
{
    public interface IEmailHelperService
    {
        Task<bool> SendDonationMail(TransactionDonationRequestDto cardKnoxDonationReceiptRequest, ResultViewModel resultViewModel);
    }
}
