using CardknoxApi;
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
    }
}
