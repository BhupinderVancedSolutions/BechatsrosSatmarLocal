namespace Infrastructure.DTO.Request.Cardknox
{
    public class CreatePaymentMethodRequestDto
    {
        public string SoftwareName { get; set; }
        public string SoftwareVersion { get; set; }
        public string CustomerId { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public string TokenAlias { get; set; }
        public string RefNum { get; set; }
        public string Exp { get; set; }
        public string Routing { get; set; }
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string SetAsDefault { get; set; }       
    }
}
