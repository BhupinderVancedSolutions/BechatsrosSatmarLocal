using System;

namespace Application.Common.Models.Response
{
    public class ResetPasswordToken
    {
        public int UserId { get; init; }
        public string PasswordToken { get; set; }
        public DateTime PasswordExpirationDate { get; set; }
    }
}
