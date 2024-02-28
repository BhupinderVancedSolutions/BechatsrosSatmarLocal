using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Attributes;
using Infrastructure.Implementation.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    [ScopedService]
    public class PaymentProviderRepository : IPaymentProviderRepository
    {
        private readonly TeamConnectEntityContext _teamConnectEntityContext;
        public PaymentProviderRepository(TeamConnectEntityContext teamConnectEntityContext)
        {
                _teamConnectEntityContext = teamConnectEntityContext;
        }
        public async Task<PaymentProvider> GetPaymentProviderByOrganizationId(int cardknoxId)
        {
            return await _teamConnectEntityContext.PaymentProviders.FirstOrDefaultAsync(x => x.CardKnoxId == cardknoxId && !x.IsDeleted);
        }
    }
}
