using System;

namespace Application.Common.DTO.Response
{
    public class ResetPasswordToken
    {
        public int UserId { get; init; }
        public string PasswordToken { get; set; }
        public DateTime PasswordExpirationDate { get; set; }
    }
}
