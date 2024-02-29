using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum PaymentMethodEnum
    {
        Cash = 1,
        Check = 2,
        ACH = 3,
        [Display(Name = "Credit Card")]
        CreditCard = 4,
        [Display(Name = "OJC Card")]
        OJC = 5,
        [Display(Name = "Donors Fund")]
        DonorsFund = 6,       
        [Display(Name = "Matbia")]
        Matbia = 7,
        [Display(Name = "Pledger")]
        Pledger = 8,
        [Display(Name = "Donors Fuse")]
        DonorsFuse =9,
        Other = 10,
    }
}
