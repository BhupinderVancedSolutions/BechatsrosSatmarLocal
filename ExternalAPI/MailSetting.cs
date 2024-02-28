namespace ExternalAPI
{
    public class MailSetting
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SendGridKey { get; set; }
        public string InviteOrganizationMessage { get; set; }
        public string WebRootPath { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool IsUsingSendGridKey { get; set; }
        public bool EnableSsl { get; set; }
    }
}
