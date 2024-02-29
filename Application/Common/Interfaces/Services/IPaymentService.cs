using DTO.Request;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<bool> Payments(TransactionRequestDto cardKnoxDonationRequest);
    }
}
