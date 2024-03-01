using CardknoxApi;
using DTO.Request;
using DTO.Response;
using Infrastructure.DTO.Response.Cardknox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services.PaymentService
{
    public interface ICardknoxPaymentService
    {
        CardknoxResponse PaymentByCreditCard(decimal amount, string cardNumber, string expirationMonth, string expirationYear, string cvv, string clientId, string clientSecret);
        Task<CardKnoxRecurringResponse> AddRecurringPayment(TransactionRequestDto cardKnoxDonationRequest);
    }
}
