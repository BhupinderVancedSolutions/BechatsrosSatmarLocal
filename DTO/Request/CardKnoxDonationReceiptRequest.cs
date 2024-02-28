
using System;

namespace DTO.Request
{
  
    public class TransactionEmailRequestDto
    {
        public int CardKnoxId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string Cell { get; set; }
        public string DeliveryName { get; set; }
        public string DeiveryAddress { get; set; }
        public string Email { get; set; }
        public int TransactionId { get; set; }
        public bool Status { get; set; }
        public string SupportEmail { get; set; }
        public string HeaderText { get; set; }
        public string DomainUrl { get; set; }
        public string LogoUrl { get; set; }
        public string Note { get; set; }
        public string DonationAmount { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string TransactionResult { get; set; }
        public string HeaderImagePath { get; set; }
        public string NameOnCard { get; set; }
        public string TransactionGuid { get; set; }

    }

}
