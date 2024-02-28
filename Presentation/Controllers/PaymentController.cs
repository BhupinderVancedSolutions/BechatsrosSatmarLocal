using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Application.Common.Interfaces.Services;
using DTO.Request;
using CardknoxApi;
using Microsoft.AspNetCore.Http;

namespace Presentation.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
                _paymentService = paymentService;
        }

        public async Task<IActionResult> ChargeCard(decimal? amount)
        {
            ChargeCardRequestDto chargeCardRequestDto = new();
            chargeCardRequestDto.Amount = amount == null || amount == 0 ? 12 : (decimal)amount;
            chargeCardRequestDto.AmountPerMonth = chargeCardRequestDto.Amount / 12;
            return View("~/Views/Payment/ChargeCard.cshtml", chargeCardRequestDto);
        }
        public async Task<IActionResult> ChargeCardform()
        {
            return View("~/Views/Payment/ChargeCard.cshtml");
        }
      
        [HttpPost]
        public async Task<IActionResult> ChargeCard(ChargeCardRequestDto chargeCardRequestDto)
        {
            //chargeCardRequestDto.ExpMonth = 12;
            //chargeCardRequestDto.ExpYear = 25;
           var res = await _paymentService.Payments(chargeCardRequestDto);

            return null;
        }

    }
}

