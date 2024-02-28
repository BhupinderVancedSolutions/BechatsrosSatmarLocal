using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class ResetPasswordRequest
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a Password.")]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Password length must be greater than or equal 6 characters.")]
        public string Password { get; init; }

        [Required(ErrorMessage = "Please enter a Confrim Password.")]
        [Display(Name = "Confrim Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password field do not match.")]

        public string ConfirmPassword { get; init; }
        public DateTime PasswordExpirationDate { get; init; }
    }
}
