namespace Common
{
    public class AppSettings
    {
        public string ApplicationUrl { get; init; }
        public string Host { get; init; }
        public bool EnableSsl { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public int Port { get; init; }
        public string ResetPassword { get; init; }
        public int DefaultPageSize { get; init; }
        public int ExpirationHours { get; init; }
        public string Secret { get; init; }
        public string ApplicationPath { get; set; }
        public int LinkValidHours { get; set; }
        public string AdminEmail { get; init; }
        public string OrganizationMemberSheetName { get; set; }
        public string WebApplicationUrl { get; set; }
        public string AngularApplicationUrl { get; set; }
        public int PageSize { get; set; }
        public string HebrewAudioFilePathMale { get; set; }
        public string HebrewAudioFilePathFemale { get; set; }
        public string ExchangedAmountBaseUrl { get; set; }
        public string ExchangedAmountKey { get; set; }
        public string ExchangedAmountSymbolCurrency { get; set; }
        public string EncryptionSecret { get; set; }
        public string CurrentYear { get; set; }
        public string BitlyAccessToken { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
