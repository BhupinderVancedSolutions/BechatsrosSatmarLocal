
using DTO.Request;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ICardKnoxService
    {
        //    Task<bool>CardKnoxDonation(CardKnoxDonationRequest cardKnoxDonationRequest);


        Task<bool> Payments(TransactionDonationRequestDto cardKnoxDonationRequest);
    }
}
