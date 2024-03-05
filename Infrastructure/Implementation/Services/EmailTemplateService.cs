using Application.Common.Interfaces.Services;
using DTO.Request;
using Infrastructure.Attributes;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Razor.Templating.Core;
using Common.Helper;
using Application.Common.Interfaces.ExternalAPI;
using Common.Settings;

namespace Infrastructure.Implementation.Services
{
    [ScopedService]
    public class EmailTemplateService : IEmailTemplateService
    {
        #region Fields

        private readonly AppSettings _appSettings;
        private readonly ISendGridEmail _sendGridEmail;


        #endregion

        #region Ctor
        public EmailTemplateService(IOptions<AppSettings> appSettings, ISendGridEmail sendGridEmail)
        {
            _appSettings = appSettings.Value;
            _sendGridEmail = sendGridEmail;
        }

        #endregion
        public async Task<bool> SendForgotPasswordMail(string name, string email, string passwordResetLink)
        {
            var body = string.Empty;
            var viewModel = new ResetPasswordModel
            {
                ResetPasswordUrl = passwordResetLink,
            };
            body = await RazorTemplateEngine.RenderAsync("/Views/EmailTemplates/ResetPassword.cshtml", viewModel);
            var isMailSent = await _sendGridEmail.SendMail(
                  email, "Password Recovery", body);

            return isMailSent;
        }

        public async Task<bool> SendTransactionMail(TransactionRequestDto transactionRequestDto, string transactionId, bool isTransactionSucceeded, string errorMessage)
        {             
                var templatePath = "Views/EmailTemplates/SendTransactionEmail.cshtml"; 
                var emailSubject = isTransactionSucceeded ? "Payment Succeeded" : "Payment Failed";                
                var body = string.Empty;
                var obj = new TransactionEmailRequestDto()
                {
                    Name = transactionRequestDto.FirstName+" "+ transactionRequestDto.LastName,
                    Address = transactionRequestDto.Address+", "+ transactionRequestDto.City+", "+ transactionRequestDto.Zip,
                    Phone = transactionRequestDto.PhoneNumber,
                    Email = transactionRequestDto.Email,                   
                    Status = (transactionRequestDto.TransactionResult == "Succeeded" && transactionRequestDto.TransactionId > 0 ? true : false),
                    DomainUrl=_appSettings.ApplicationUrl,
                    Amount= transactionRequestDto.Amount,
                    TransactionId= transactionId

                };
                body = await RazorTemplateEngine.RenderAsync(templatePath, obj);
                var pdf = CommonHelper.CreatePdfUsingSelectHtmlToPdf(body);
                var mail = await _sendGridEmail.SendMail(transactionRequestDto.Email, emailSubject, body, "", null);
                return mail;
        }

    }
}
