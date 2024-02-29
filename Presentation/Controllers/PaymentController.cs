
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using DTO.Request;

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
            ViewData["ReturnUrl"] = amount;
            TransactionRequestDto chargeCardRequestDto = new();
            chargeCardRequestDto.Amount = amount == null || amount == 0 ? 12 : (decimal)amount;
            chargeCardRequestDto.AmountPerMonth = chargeCardRequestDto.Amount / 12;
            return View("~/Views/Payment/ChargeCard.cshtml", chargeCardRequestDto);
        }             
        [HttpPost]
        public async Task<IActionResult> ChargeCard(TransactionRequestDto chargeCardRequestDto)
        {
            var result=await _paymentService.Payments(chargeCardRequestDto);
            return Json(result);
        }

    }
}

