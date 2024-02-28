using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request
{
    public class TransactionDonationRequestDto
    {
        public int CardKnoxId { get; set; }
        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a Home Phone.")]
        public string HomePhone { get; set; }
        [Required(ErrorMessage = "Please enter a Cell.")]
        public string Cell { get; set; }
        [Required(ErrorMessage = "Please enter a Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a Expmonth.")]
        public long ExpMonth { get; set; }
        [Required(ErrorMessage = "Please enter a ExpYear.")]
        public long ExpYear { get; set; }
        [Required(ErrorMessage = "Please enter a CreditCardNumber.")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Please enter a CVV.")]
        public string Cvv { get; set; }
        public string CC { get; set; }
        public string CardLast4Digit { get; set; }
        public byte CurrencyType { get; set; } = 1; 
        public string IPAddress { get; set; }
        public string DeliveryName { get; set; }
        public string DeiveryAddress { get; set; }
        public int TransactionId { get; set; }
        public decimal DonationAmount { get; set; }
        public string TransactionGuid { get; set; }

        public string TransactionResult { get; set; }
        public bool Status { get; set; }
        public string SupportEmail { get; set; }
        public string ExpDate { get; set; }
 



    }
}
