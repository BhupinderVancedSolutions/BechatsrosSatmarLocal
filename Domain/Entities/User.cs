using System;

namespace Domain.Entities
{
    public class User:BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirm { get; set; }
        public string PasswordToken { get; set; }
        public string TeamConnectToken { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }

    }
}
