using DTO.Request;
using DTO.Response.Transactions;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<TransactionResponseDto> Payments(TransactionRequestDto cardKnoxDonationRequest);
    }
}
