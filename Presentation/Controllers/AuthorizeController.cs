using Common.Helper;
using Mapster;
using DTO.Request;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization;
using System.Threading.Tasks;
using Application.Authentication.Queries;
using Application.Authentication.Commands;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Application.Common.Interfaces.Services;
using Common;
using System;

namespace Presentation.Controllers
{
    public class AuthorizeController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly UserRequest _user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthorizeController(IOptions<AppSettings> appSettings,IOptions<UserRequest> user, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _user = httpContextAccessor.HttpContext.Session.GetObjectFromJson<UserRequest>("LoggedInUserDetails", _appSettings.Secret);
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewBag.ErrMsg = TempData["ErrorMessage"] != null ? TempData["ErrorMessage"].ToString() : string.Empty;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthenticateUserRequest authenticateUserRequest, string returnUrl = null)
        {
            var command = authenticateUserRequest.Adapt<AuthenticateUserCommand>();
            var result = await Mediator.Send(command);
            ViewBag.ErrMsg = result.ErrorMessage;
            if (result.IsSuccess)
            {
                var userDetail = result.Result.Adapt<UserRequest>();
                HttpContext.Session.SetObjectAsJson("LoggedInUserDetails", userDetail, _appSettings.Secret);               
                ViewBag.SecretKey = _appSettings.Secret;
                return RedirectToAction("Index", "Home");
            }
            return View(authenticateUserRequest);
        }

        [CustomAuthorization]
        [HttpGet]
        public IActionResult LogOut()
        {
            //CommonHelper.RemoveKeyFromSession(HttpContext, "LoggedInUserDetails");           
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorize");
        }

        [HttpGet]
        public  IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="forgetPasswordRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgetPasswordRequest forgetPasswordRequest)
        {
            var passwordToken = EncryptionManager.CreateHash(forgetPasswordRequest.Email);
            var passwordResetLink = Url.Action("ResetPassword", "Authorize", new { token = passwordToken }, Request.Scheme);
            forgetPasswordRequest.PasswordToken = passwordToken;
            forgetPasswordRequest.PasswordResetLink = passwordResetLink;
            var command = forgetPasswordRequest.Adapt<ForgotPasswordCommand>();
            var result = await Mediator.Send(command);
            if (result.Succeeded == true)
            {
                ViewBag.SuccessMsg = "Please check you email for password Reset.";
            }
            else
            {
                ViewBag.ErrorMsg = "Email does not Exist.";
            }
            return View();
        }

        /// <summary>
        /// 
        /// Reset Password
        /// </summary>
        /// <param name="passwordToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token = "")
        {
            if(_user != null)
            {
                return RedirectToAction("ResetPassword", "User");
            }
            else
            {
                var query = new ResetPasswordQuery { PasswordToken = token };
                var result = await Mediator.Send(query);
                if (result == null)
                {
                    ViewBag.ErrorMsg = "Link has been expired";
                }
                else
                {
                    if (_appSettings.LinkValidHours >= (DateTime.Now - (DateTime)result.PasswordExpirationDate).Hours)
                    {
                        var userDetail = result.Adapt<UserRequest>();
                        HttpContext.Session.SetObjectAsJson("LoggedInUserDetails", userDetail, _appSettings.Secret);
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Link has been expired";
                    }

                }
                return View();
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            resetPasswordRequest.UserId = _user.UserId;
            var command = resetPasswordRequest.Adapt<ResetPasswordCommand>();
            var res = await Mediator.Send(command);
            if (res.Succeeded == true)
            {
                ViewBag.SucessMsg = "Password updated successfully";
                return View() ;
            }
            else
            {
                ViewBag.ErrorMsg = "Link has been expired";
                return View();
            }

        }
        [HttpGet]
        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

    }
}
