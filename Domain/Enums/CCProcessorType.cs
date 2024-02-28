using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum CCProcessorType
    {
        OJC = 1,
        Stripe = 3,
        AuthorizeNet = 5,
        Cardknox = 6,
        USAePAY = 7,
        SequelPay = 8,
        DonorsFund = 9,
        Matbia = 10,
        Pledger = 11,
        DonorsFuse = 12
    }
}
