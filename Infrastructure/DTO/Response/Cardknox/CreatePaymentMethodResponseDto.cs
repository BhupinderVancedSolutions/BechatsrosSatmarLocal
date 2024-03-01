namespace Infrastructure.DTO.Response.Cardknox
{
    public class CreatePaymentMethodResponseDto
    {
        public string RefNum { get; set; }
        public string Result { get; set; }
        public string Error { get; set; }
        public string PaymentMethodId { get; set; }
    }
}
