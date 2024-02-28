using Application.Common.Interfaces.Services.PaymentService;
using CardknoxApi;
using CardknoxApi.Operations;
using Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services.PaymentService
{
    [ScopedService]
    public class CardknoxPaymentService : ICardknoxPaymentService
    {
        #region Method

        public CardknoxResponse PaymentByCreditCard(decimal amount, string cardNumber, string expirationMonth, string expirationYear, string cvv, string clientId, string clientSecret)
        {
            if (expirationYear.Length > 2)
            {
                expirationYear = expirationYear[2..];
            }
            CCSale cCSale = new CCSale
            {
                Amount = amount,
                CardNum = cardNumber,
                Exp = $"{expirationMonth}{expirationYear}",
                CVV = cvv,

               
            };

            CardknoxRequest cardknoxRequest = new CardknoxRequest(clientId, clientSecret, "1.0.1");
            Cardknox cardknox = new Cardknox(cardknoxRequest);
            return cardknox.CCSale(cCSale);
        }

        #endregion
    }
}
