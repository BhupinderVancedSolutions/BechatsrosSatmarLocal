using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using DTO.Request;
using Infrastructure.Attributes;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    [ScopedService]
    public class PaymentProviderService : IPaymentProviderService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentProviderRepository _paymentProviderRepository;
        public PaymentProviderService(IMapper mapper , IPaymentProviderRepository paymentProviderRepository)
        {
                _mapper = mapper;
              _paymentProviderRepository = paymentProviderRepository;
        }
        public async Task<PaymentProviderRequest> GetPaymentProviderByCardKonxId(int cardKonxId)
        {
            var paymentProvider = await _paymentProviderRepository.GetPaymentProviderByOrganizationId(cardKonxId);
            return _mapper.Map<PaymentProviderRequest>(paymentProvider);
        }
    }
}
