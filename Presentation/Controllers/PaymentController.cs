using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using DTO.Request;
using Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Common.Helper;
using System;

namespace Presentation.Controllers

{
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly AppSettings _appSettings;
        private readonly UserRequest _user;
        public PaymentController(IPaymentService paymentService, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _paymentService = paymentService;
            _appSettings = appSettings.Value;
            _user = httpContextAccessor.HttpContext.Session.GetObjectFromJson<UserRequest>("LoggedInUserDetails", _appSettings.Secret);
        }

        public async Task<IActionResult> ChargeCard()
        {
           // ViewData["ReturnUrl"] = amount;
            TransactionRequestDto transactionRequestDto = new();
           // transactionRequestDto.Amount = amount == null || amount == 0 ? 12 : (decimal)amount;
            //transactionRequestDto.AmountPerMonth = Math.Round(transactionRequestDto.Amount / 12, 2);
          //  transactionRequestDto.UserId = _user?.UserId ?? 0;
            return View("~/Views/Payment/ChargeCard.cshtml", transactionRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> ChargeCard(TransactionRequestDto transactionRequestDto)
        {

            var result = await _paymentService.Payments(transactionRequestDto);
            return Json(new { IsError = result.IsError, Message = result.Reason, error = result.ErrorMessage });
        }

    }
}

