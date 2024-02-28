using Application.Common.Interfaces.Services.HelperService;
using Application.Common.Models.Response;
using Common;
using Common.Helper;
using DTO.Request;
using DTO.Response;
using ExternalAPI;
using Microsoft.Extensions.Options;
using Razor.Templating.Core;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services.HelperService
{
    public class EmailHelperService : IEmailHelperService
    {
        #region fields
        private readonly MailSetting _mailSetting;
        #endregion
        public EmailHelperService(IOptions<MailSetting> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }

        public async Task<bool> SendDonationMail(TransactionDonationRequestDto cardKnoxDonationReceiptRequest, ResultViewModel resultViewModel)
        {
            try
            {
                var DonationReceipt = "Views/EmailTemplates/SendDonationEmail.cshtml";

                var templatePath = DonationReceipt;
                var emailSubject = resultViewModel.Status ? $"Official donation receipt from {cardKnoxDonationReceiptRequest.Name}" : $"{cardKnoxDonationReceiptRequest.Name} Donation Failed";
                var organizatioEmailSubject = resultViewModel.Status ? "Congratulations! New donation Towards you" : "Donation Failed";
                var body = string.Empty;

                var obj = new TransactionEmailRequestDto()
                {
                    Name = cardKnoxDonationReceiptRequest.Name,
                    Address = cardKnoxDonationReceiptRequest.Address,
                    HomePhone = cardKnoxDonationReceiptRequest.HomePhone,
                    Cell = cardKnoxDonationReceiptRequest.Cell,
                    Email = cardKnoxDonationReceiptRequest.Email,
                    TransactionGuid = cardKnoxDonationReceiptRequest.TransactionGuid,
                    Status = (cardKnoxDonationReceiptRequest.TransactionResult == "succeeded" && cardKnoxDonationReceiptRequest.TransactionId > 0 ? true : false),

                };
                body = await RazorTemplateEngine.RenderAsync(templatePath, obj);
                var pdf = CommonHelper.CreatePdfUsingSelectHtmlToPdf(body);
              return await EmailHelper.SendMailWithAttachment(_mailSetting.FromEmail, cardKnoxDonationReceiptRequest.Email, _mailSetting.SendGridKey, emailSubject, body, pdf, $"{emailSubject}.pdf", "", null);
              
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}