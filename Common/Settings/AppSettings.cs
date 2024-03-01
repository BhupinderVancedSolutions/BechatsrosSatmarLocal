namespace Common.Settings
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
        public string EncryptionSecret { get; set; }
        public string CurrentYear { get; set; }    
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SoftwareVersion { get; set; }
        public string SoftwareName { get; set; }
    }
}
