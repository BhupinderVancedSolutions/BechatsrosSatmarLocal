
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class ChargeCardRequestDto
    {
        public int CardKnoxId { get; set; }
        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a Email.")]
        public string Email { get; set; }
        [Display(Name = "Expiry Month")]
        [Required(ErrorMessage = "Please enter Expiry Month.")]
        public long ExpMonth { get; set; }
        [Display(Name = "Expiry Year")]
        [Required(ErrorMessage = "Please enter Expiry Year.")]
        public long ExpYear { get; set; }
        [Display(Name = "Credit Card Number")]
        [Required(ErrorMessage = "Please enter Credit Card Number.")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Please enter CVV.")]
        public string Cvv { get; set; }
        [Required(ErrorMessage = "Please enter a PhoneNumber.")]
        public string PhoneNumber { get; set; }
        public string CardLast4Digit { get; set; }
        public string ExpDate { get; set; }
        [Required(ErrorMessage = "Please enter a FirstName.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a LastName.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter a City.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a Zip.")]
        public int Zip { get; set; }

    }
}
