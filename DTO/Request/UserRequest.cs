using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class UserRequest
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; init; }
        public string LastName { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}", ErrorMessage = "Please enter a valid Email Address.")]
        public string Email { get; init; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set; }
        public string ProfileImagePath { get; set; }
        public string Token { get; set; }           
        public string Name { get; init; }
        public string RoleDescription { get; set; }        
        public bool IsSuperAdmin { get; set; }
        public DateTime PasswordExpirationDate { get; set; }
        public string PasswordToken { get; set; }
        public string ConfirmationLink { get; set; }
        public byte[] Password { get; set; }       
        public int AccountId { get; set; } = 0;
        public int? LoginUserId { get; set; }        
    }
}
