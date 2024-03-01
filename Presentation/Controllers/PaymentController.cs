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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentController(IPaymentService paymentService, IOptions<AppSettings> appSettings, IOptions<UserRequest> user, IHttpContextAccessor httpContextAccessor)
        {
            _paymentService = paymentService;
            _appSettings = appSettings.Value;
            _user = httpContextAccessor.HttpContext.Session.GetObjectFromJson<UserRequest>("LoggedInUserDetails", _appSettings.Secret);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ChargeCard(decimal? amount)
        {
            ViewData["ReturnUrl"] = amount;
            TransactionRequestDto chargeCardRequestDto = new();
            chargeCardRequestDto.Amount = amount == null || amount == 0 ? 12 : (decimal)amount;
            chargeCardRequestDto.AmountPerMonth = Math.Round(chargeCardRequestDto.Amount / 12, 2);
            chargeCardRequestDto.UserId = _user?.UserId ?? 0;
            return View("~/Views/Payment/ChargeCard.cshtml", chargeCardRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> ChargeCard(TransactionRequestDto chargeCardRequestDto)
        {

            var result=await _paymentService.Payments(chargeCardRequestDto);
            return Json(new { Status = result.Status, Message = result.Reason });
        }

    }
}

