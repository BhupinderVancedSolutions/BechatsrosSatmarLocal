using System;

namespace DTO.Response
{
    public class ResetPasswordTokenResponse
    {
        public int UserId { get; init; }
        public string PasswordToken { get; init; }
        public DateTime PasswordExpirationDate { get; init; }
    }
}
