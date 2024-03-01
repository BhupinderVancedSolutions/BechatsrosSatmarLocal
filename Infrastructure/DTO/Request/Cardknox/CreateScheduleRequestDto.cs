namespace Infrastructure.DTO.Request.Cardknox
{
    public class CreateScheduleRequestDto
    {
        public string SoftwareName { get; set; }
        public string SoftwareVersion { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string IntervalType { get; set; }
        public int IntervalCount { get; set; }
        public string ScheduleName { get; set; }
        public int TotalPayments { get; set; }
        public bool SkipSaturdayAndHolidays { get; set; }
        public bool AllowInitialTransactionToDecline { get; set; }
        public bool CustReceipt { get; set; }
        //public string Custom01 { get; set; }
        //public string Custom02 { get; set; }
        //public string Custom03 { get; set; }			
    }
}
