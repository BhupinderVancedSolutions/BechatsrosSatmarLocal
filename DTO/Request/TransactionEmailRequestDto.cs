
using System;

namespace DTO.Request
{
  
    public class TransactionEmailRequestDto
    {       
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }                
        public string Email { get; set; }
        public string TransactionId { get; set; }
        public bool Status { get; set; }
        public string SupportEmail { get; set; }        
        public string DomainUrl { get; set; }
        public string LogoUrl { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string TransactionResult { get; set; }               
    }

}
