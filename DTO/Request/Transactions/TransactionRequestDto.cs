﻿using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class TransactionRequestDto
    {
        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a Card Title")]
        [MaxLength(20, ErrorMessage = "Card Title must be at most 20 characters long")]
        public string CardTitle { get; set; }

        [Required(ErrorMessage = "Please enter an Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a Email")]
        [RegularExpression("[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}", ErrorMessage = "Please enter a valid Email Address.")]
        [MaxLength(50, ErrorMessage = "Email maximum length of 50.")]
        public string Email { get; set; }

        [Display(Name = "Expiry Month")]
        [Required(ErrorMessage = "Please enter the Expiry Month.")]
        [Range(1, 12, ErrorMessage = "Expiry Month should be between 1 and 12.")]
        public int ExpMonth { get; set; }

        [Required(ErrorMessage = "Please enter the Expiry Year.")]
        [Range(2022, 2100, ErrorMessage = "Expiry Year should be between 2022 and 2100.")]
        public int ExpYear { get; set; }

        [Required(ErrorMessage = "Please enter a Card Number.")]
        [RegularExpression("^[0-9]{12,16}$", ErrorMessage = "Card Number must be between 12 and 16 digits.")]
        [DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Please enter a CVV.")]
        [MinLength(3, ErrorMessage = "Cvv minimum length of 3.")]
        [MaxLength(4, ErrorMessage = "Cvv maximum length of 4.")]
        public string Cvv { get; set; }

        [Required(ErrorMessage = "Please enter a Phone Number.")]
        [RegularExpression("^[0-9]{0,10}$", ErrorMessage = "Phone Number must be between 10 digits.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter Expiry Date.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Expiry Date must be 4 digits.")]
        public string ExpDate { get; set; }

        [Required(ErrorMessage = "Please enter a First Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a City.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a Zip.")]
        [MinLength(5, ErrorMessage = "Zip minimum length of 3.")]
        [MaxLength(5, ErrorMessage = "Zip maximum length of 4.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip Code must be 5 digits.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a Donation Amount.")]
        [Range(1, 999999, ErrorMessage = "Donation Amount should be between 1 and 999999.")]
        public decimal Amount { get; set; }
        public decimal AmountPerMonth { get; set; }
        public int TransactionId { get; set; }
        public string TransactionGuid { get; set; }
        public string TransactionResult { get; set; }
        [Required(ErrorMessage = "Please enter a Name.")]
        public string DeliveryName { get; set; }
        [Required(ErrorMessage = "Please enter an Address")]
        public string DeliveryAddress { get; set; }
        [Required(ErrorMessage = "Please enter a  City.")]
        public string DeliveryCity { get; set; }
        [Required(ErrorMessage = "Please enter a Zip.")]
        [MinLength(5, ErrorMessage = "Zip minimum length of 3.")]
        [MaxLength(5, ErrorMessage = "Zip maximum length of 4.")]
        public string DeliveryZip { get; set; }
        public bool IsDeliveryAddress { get; set; }
        public bool IsAutoRenew { get; set; }
        public int? UserId { get; set; }
        public bool Status { get; set; }

    }
}
