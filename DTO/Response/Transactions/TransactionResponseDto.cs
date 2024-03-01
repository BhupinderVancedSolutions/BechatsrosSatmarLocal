namespace DTO.Response.Transactions
{
    public class TransactionResponseDto
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }       
        public decimal Amount { get; set; }
        public string TransactionResult { get; set; }
        public string TransactionGuid { get; set; }          
        public string TransactionType { get; set; }       
        public int? PaymentMethodId { get; set; }
        public int? ItemTypeId { get; set; }      
        public int? CCProcessorId { get; set; }             
        public string Reason { get; set; }          
        public bool IsTransactionSucceeded { get; set; }
        public bool Status { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

    }
}
