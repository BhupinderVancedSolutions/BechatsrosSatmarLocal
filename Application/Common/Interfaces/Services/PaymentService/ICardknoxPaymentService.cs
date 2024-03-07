using CardknoxApi;
using DTO.Request;
using DTO.Response;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services.PaymentService
{
    public interface ICardknoxPaymentService
    {
        CardknoxResponse PaymentByCreditCard(TransactionRequestDto transactionRequestDto);
        Task<CardKnoxRecurringResponse> AddRecurringPayment(TransactionRequestDto cardKnoxDonationRequest);
    }
}
