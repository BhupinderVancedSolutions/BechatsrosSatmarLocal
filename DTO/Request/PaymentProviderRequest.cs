using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request
{
    public class PaymentProviderRequest
    {
        public int PaymentProviderId { get; set; }
        [DisplayName("Payment Type")]
        [Required(ErrorMessage = "Please enter a PaymentProviderTypeId.")]
        public int PaymentProviderTypeId { get; set; }
        public int CardKnoxId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string ClientId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string ClientSecret { get; set; }
        public bool IsMasterPaymentProvider { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string UserId { get; set; }
  
        [DisplayName("Accept OJC")]
        public bool IsOJC { get; set; }
        public bool IsRecurringDonationsActiveForOrganization { get; set; }
        public bool IsMasterConnectedAccount { get; set; }

        [DisplayName("Donors Fund Key")]
        public string DonorFundKey { get; set; }

        [DisplayName("Accept Donors Fund")]
        public bool IsDonorFund { get; set; }
        [DisplayName("Matbia Key")]
        public string MatbiaKey { get; set; }
        [DisplayName("Accept Matbia")]
        public bool IsMatbia { get; set; }

        [DisplayName("Accept Pledger")]
        public bool IsPledger { get; set; }

        [DisplayName("Pledger Key")]
        public string PledgerKey { get; set; }
    }
}
