using DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IPaymentProviderService
    {
        Task<PaymentProviderRequest> GetPaymentProviderByCardKonxId(int cardKonxId);
    }
}
