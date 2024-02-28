using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PaymentMethod
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
        Other = 10,
        [Display(Name = "Matbia")]
        Matbia = 8,
        [Display(Name = "Pledger")]
        Pledger = 9,
        [Display(Name = "Donors Fuse")]
        DonorsFuse = 11,
    }
}
