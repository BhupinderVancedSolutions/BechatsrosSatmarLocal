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
    public class CardKnoxController : ControllerBase
    {
        private readonly ICardKnoxService _cardKnoxService;
        public CardKnoxController(ICardKnoxService cardKnoxService)
        {
                _cardKnoxService = cardKnoxService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> CardKnoxDonationform()
        {
            return View("~/Views/CardKnox/CardKnoxDonationform.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CardKnoxDonation(TransactionDonationRequestDto cardKnoxDonationRequest)
        {
            cardKnoxDonationRequest.ExpMonth = 12;
            cardKnoxDonationRequest.ExpYear = 25;
            var res = await _cardKnoxService.Payments(cardKnoxDonationRequest);

            return Json(res);
        }

    }
}

