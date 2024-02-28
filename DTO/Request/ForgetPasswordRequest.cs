using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class ForgetPasswordRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}", ErrorMessage = "Please enter a valid Email Address.")]
        public string Email { get; init; }
        public string PasswordToken { get; set; }
        public string PasswordResetLink { get; set; }
    }
}
