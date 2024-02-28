
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentProvider : BaseEntity
    {
        [Key]
        public int PaymentProviderId { get; set; }
        public int CardKnoxId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool IsMasterPaymentProvider { get; set; }
        public bool IsDeleted { get; set; }
        public int PaymentProviderTypeId { get; set; }
    }
}
