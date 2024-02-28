using DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response
{
    public class TransactionResponse
    {
        public CardDetails CardDetails { get; set; }
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string Token { get; set; }
        public int MerchantKey { get; set; }
        public string TransactionIpAddress { get; set; }
        public string HostCode { get; set; }
        public string ResponseMessage { get; set; }
        public int ResultCode { get; set; }
        public string ResultText { get; set; }
        public InvoiceData InvoiceData { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
        public string Register { get; set; }
        public int ResellerKey { get; set; }
        public string PaymentType { get; set; }
        public string Processor { get; set; }
        public string HostReferenceNumber { get; set; }
    }
}
