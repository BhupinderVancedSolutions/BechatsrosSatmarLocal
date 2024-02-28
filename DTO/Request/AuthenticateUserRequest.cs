using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class AuthenticateUserRequest
    {
        [Required(ErrorMessage = "Please enter a Email.")]
        [RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}", ErrorMessage = "Please enter a valid Email Address.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Please enter a Password.")]
        public string Password { get; init; }
    }
}
