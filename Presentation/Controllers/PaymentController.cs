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
        private readonly ICardKnoxService _cardKnoxService;
        public PaymentController(ICardKnoxService cardKnoxService)
        {
                _cardKnoxService = cardKnoxService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ChargeCard()
        {
            return View("~/Views/Payment/ChargeCard.cshtml");
        }
        public async Task<IActionResult> ChargeCardform()
        {
            return View("~/Views/Payment/ChargeCard.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> ChargeCardDonation(TransactionDonationRequestDto cardKnoxDonationRequest)
        {
            cardKnoxDonationRequest.ExpMonth = 12;
            cardKnoxDonationRequest.ExpYear = 25;
            var res = await _cardKnoxService.Payments(cardKnoxDonationRequest);

            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> ChargeCard(ChargeCardRequestDto chargeCardRequestDto)
        {
            chargeCardRequestDto.ExpMonth = 12;
            chargeCardRequestDto.ExpYear = 25;
           //var res = await _cardKnoxService.Payments(chargeCardRequestDto);

            return null;
        }


    }
}

