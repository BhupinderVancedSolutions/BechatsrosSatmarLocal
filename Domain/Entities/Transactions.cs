

using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Transactions : BaseEntity
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public int? CardKnoxId { get; set; }
        public decimal DonationAmount { get; set; }
        public string TransactionResult { get; set; }
        public string TransactionGuid { get; set; }
        public string CustomerId { get; set; }
        public bool DeletedByDonor { get; set; }
        public bool DeletedByOrganization { get; set; }
        public string TransactionType { get; set; }
        public string Receipt { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? ItemTypeId { get; set; }
        public virtual ItemType ItemTypes { get; set; }
        public int? CCProcessorId { get; set; }
        public Guid TransactionInternalGuid { get; set; }
        public string StripeAccountId { get; set; }
        public string Reason { get; set; }
        [ForeignKey("CCProcessorId")]
        public virtual CCProcessorType CCProcessorTypes { get; set; }
        public bool? IsRecurred { get; set; }
        public bool? IsAddedByService { get; set; } = false;
        public string ReceiptName { get; set; }
        public int? DonationRequestId { get; set; }
        public string IPAddress { get; set; }
        public string StopRecurringNote { get; set; }
        public decimal? CurrencyAmount { get; set; }
        public byte? CurrencyType { get; set; }
        public string Last4 { get; set; }
        public bool? IsRecurNow { get; set; }
    }
}
