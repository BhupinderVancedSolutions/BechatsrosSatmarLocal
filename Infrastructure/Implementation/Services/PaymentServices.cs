using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Services.PaymentService;
using CardknoxApi;
using Common.Enums;
using Common.Helper;
using Common.Settings;
using Domain.Enums;
using DTO.Request;
using DTO.Response;
using DTO.Response.Transactions;
using Infrastructure.Attributes;
using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    [ScopedService]
    public class PaymentServices : IPaymentService
    {
 
        private readonly ICardknoxPaymentService _cardknoxPaymentService;
        private readonly AppSettings _appSettings;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly CardknoxSetting _cardknoxSetting;
        public PaymentServices( ICardknoxPaymentService cardknoxPaymentService, IOptions<AppSettings> appSettings, IEmailTemplateService emailTemplateService, IOptions<CardknoxSetting> cardknoxSetting)
        {
                _cardknoxPaymentService = cardknoxPaymentService;
                _appSettings = appSettings.Value;
            _emailTemplateService = emailTemplateService;
            _cardknoxSetting = cardknoxSetting.Value;
        }      

        public async Task<TransactionResponseDto> Payments(TransactionRequestDto transactionRequestDto)
        {            
            TransactionResponseDto transaction = new TransactionResponseDto();
            CardknoxResponse cardknoxResponse;
            bool isTransactionSucceeded=false;
            if (transactionRequestDto.IsAutoRenew)
            {
                var result = await _cardknoxPaymentService.AddRecurringPayment(transactionRequestDto);
                if (result.IsError)
                {
                    transaction.IsError = result.IsError;
                    transaction.ErrorMessage = result.ErrorMessage;
                } 
                else
                {
                    transaction.IsError = result.IsError;
                    isTransactionSucceeded = true;
                }
                if (_appSettings.IsSendEmail == true)
                {
                    var resultViewModel = await SendDonationEmail(transactionRequestDto, result.RefNum, isTransactionSucceeded, transaction.ErrorMessage);
                }
            }
            else
            {
                (transaction, cardknoxResponse) = await PaymentByCardknox(transactionRequestDto);
                if (transaction.TransactionResult == "Failed")
                {
                    transaction.IsError = true;
                    transaction.ErrorMessage = transaction.Reason;
                }
                else
                {
                    isTransactionSucceeded = true;
                }
                if (_appSettings.IsSendEmail == true)
                {
                    var resultViewModel = await SendDonationEmail(transactionRequestDto, transaction.TransactionGuid, isTransactionSucceeded, transaction.ErrorMessage);
                }
            }
            return (transaction);

        }
        #region Private
        private async Task<(TransactionResponseDto, CardknoxResponse)> PaymentByCardknox(TransactionRequestDto transactionRequestDto)
        {
            try
            {
                if (transactionRequestDto.IsDeliveryAddress)
                {
                    transactionRequestDto.FirstName = transactionRequestDto.DeliveryName;
                    transactionRequestDto.LastName = "";
                    transactionRequestDto.Address = transactionRequestDto.DeliveryAddress;
                    transactionRequestDto.City = transactionRequestDto.DeliveryCity;
                    transactionRequestDto.Zip = transactionRequestDto.DeliveryZip;                
                }

                var transactions = new TransactionResponseDto();
                var response = _cardknoxPaymentService.PaymentByCreditCard(transactionRequestDto);
                bool isTransactionSucceeded = string.IsNullOrEmpty(response.Error);
                transactions.TransactionGuid = response.RefNum;
                transactions.CCProcessorId = (int)CCProcessorTypeEnum.Cardknox;
                transactions.PaymentMethodId = (int)PaymentMethodEnum.CreditCard;
                transactions.TransactionResult = isTransactionSucceeded ? Enum.GetName(typeof(TransactionResultEnum), TransactionResultEnum.Succeeded) : Enum.GetName(typeof(TransactionResultEnum), TransactionResultEnum.Failed);
                transactions.TransactionType = "Card";
                transactions.ItemTypeId = isTransactionSucceeded ? (int)TransactionStatusEnum.Success : (int)TransactionStatusEnum.Failed;
                transactions.Reason = isTransactionSucceeded ? null : $"Error : {response.Error}, ErrorCode : {response.ErrorCode}";
                transactions.IsTransactionSucceeded = isTransactionSucceeded;
                transactions.Status = transactionRequestDto.Status;
                
                return (transactions, response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<ResultViewModel> SendDonationEmail (TransactionRequestDto cardKnoxDonationRequest, string transactionId, bool isTransactionSucceeded, string errorMessage)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.Status = true;
            await _emailTemplateService.SendTransactionMail(cardKnoxDonationRequest, transactionId, isTransactionSucceeded, errorMessage);
            return resultViewModel;
        }

      

    #endregion
}
}
