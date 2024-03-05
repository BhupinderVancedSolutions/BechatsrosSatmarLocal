using CardknoxApi;
using DTO.Request;
using DTO.Response;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services.PaymentService
{
    public interface ICardknoxPaymentService
    {
        CardknoxResponse PaymentByCreditCard(decimal amount, string cardNumber, string expirationMonth, string expirationYear, string cvv, string xkey, string clientSecret);
        Task<CardKnoxRecurringResponse> AddRecurringPayment(TransactionRequestDto cardKnoxDonationRequest);
    }
}
