using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Services.PaymentService;
using CardknoxApi;
using Common.Helper;
using Common.Settings;
using Domain.Entities;
using Domain.Enums;
using DTO.Request;
using DTO.Response;
using Infrastructure.Attributes;
using Microsoft.Extensions.Options;
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

        public PaymentServices( ICardknoxPaymentService cardknoxPaymentService, IOptions<AppSettings> appSettings, IEmailTemplateService emailTemplateService)
        {
                _cardknoxPaymentService = cardknoxPaymentService;
                _appSettings = appSettings.Value;
            _emailTemplateService = emailTemplateService;
        }

        #region Donation Method 


        //public async Task<bool> CardKnoxDonation(CardKnoxDonationRequest cardKnoxDonationRequest)
        //{
        //    bool status = true;
        //    int? transactionRawDataId = null;
        //    int transactionId = 0;
        //    Transactions transaction = new Transactions();
        //    ResultViewModel result = new ResultViewModel();
        //    string RawResponse = string.Empty;
        //    string ExchangedAmountSymbolCurrency = _appSettings.ExchangedAmountSymbolCurrency;
        //    byte CurrencyType = cardKnoxDonationRequest.CurrencyType;
        //    //var org = organizationDetail;

        //    decimal exchangedRate = await CommonHelper.GetExchangedValue(_appSettings.ExchangedAmountBaseUrl, _appSettings.ExchangedAmountKey, ExchangedAmountSymbolCurrency,CurrencyType);
        //    decimal exchangeAmount = CommonHelper.GetExchangedAmount(exchangedRate, cardKnoxDonationRequest.DonationAmount);
        //    cardKnoxDonationRequest.DonationAmount = exchangeAmount;

        //    _logger.Information("Processing donation for donor {DonorName} donating {DonationAmount} dollar towards to Organization Id {OrganizationId} from saved credit card Id {Id} ", cardKnoxDonationRequest.Name, cardKnoxDonationRequest.DonationAmount, cardKnoxDonationRequest.CardKnoxId, cardKnoxDonationRequest.CreditCardId);

        //    var creditCard = await _paymentMethodRepository.FindCreditCardById(cardKnoxDonationRequest.CreditCardId);
        //    if (creditCard != null)
        //    {
        //        var card = await _userPaymentInfoRepository.FindUserPaymentInfoByCreditCardId(cardKnoxDonationRequest.CreditCardId);
        //        cardKnoxDonationRequest.ExpMonth = long.Parse(EncryptionHelper.DecryptByteArrayToString(card.ExpMonth, _appSettings.EncryptionSecret));
        //        cardKnoxDonationRequest.ExpYear = long.Parse(EncryptionHelper.DecryptByteArrayToString(card.ExpYear, _appSettings.EncryptionSecret));
        //        cardKnoxDonationRequest.CreditCardNumber = $"{EncryptionHelper.DecryptByteArrayToString(creditCard.CardSecret, _appSettings.EncryptionSecret)}{EncryptionHelper.DecryptByteArrayToString(card.SecretLast4, _appSettings.EncryptionSecret)}";
        //        cardKnoxDonationRequest.Cvv = EncryptionHelper.DecryptByteArrayToString(creditCard.CVV, _appSettings.EncryptionSecret);
        //    }
        //    ( status) = await Payments(cardKnoxDonationRequest, "");
        //    if (string.IsNullOrEmpty(transaction.Reason))
        //    {
        //        _logger.Information("Donation success for donor {DonorName} donating {DonationAmount} dollar towards to Organization Id {OrganizationId} from saved credit card Id {Id} ", cardKnoxDonationRequest.Name, cardKnoxDonationRequest.DonationAmount, cardKnoxDonationRequest.CardKnoxId, cardKnoxDonationRequest.CreditCardId);
        //    }
        //    else
        //    {
        //        _logger.Error("Donation Fail due to " + transaction.Reason + " for donor {DonorName} donating {DonationAmount} dollar towards to Organization Id {OrganizationId} from saved credit card Id {Id} ", cardKnoxDonationRequest.Name, cardKnoxDonationRequest.DonationAmount, cardKnoxDonationRequest.CardKnoxId, cardKnoxDonationRequest.CreditCardId);
        //    }
        //    _logger.Information("------------------ End One time Organization Donation with saved card for donor {DonorName}  ", cardKnoxDonationRequest.Name + "---------------------------");

        //        transaction.CardKnoxId = cardKnoxDonationRequest.CardKnoxId;
        //        transaction.CurrencyType = cardKnoxDonationRequest.CurrencyType;
        //        transaction.CurrencyAmount = cardKnoxDonationRequest.DonationAmount;
        //        transaction.UserId = cardKnoxDonationRequest.UserId;
        //        transaction.IPAddress = cardKnoxDonationRequest.IPAddress;
        //        transaction.Last4 = cardKnoxDonationRequest.CreditCardNumber[^4..];
        //        //transaction.CreatedDate = DateTime.Now;
        //        transaction.DonationAmount = exchangeAmount;
        //        try
        //        {
        //            var transactionRawData = await _transactionRawDataRepository.AddTransactionRawData(new TransactionRawData { CCProcessorId = (int)Domain.Enums.CCProcessorType.DonorsFund, DonorId = cardKnoxDonationRequest.UserId, Email = organizationDonationWithSavedCardViewModel.Email, OrganizationId = cardKnoxDonationRequest.CreditCardId, RawRequest = JsonConvert.SerializeObject(cardKnoxDonationRequest), RawPaymentProvider = "DonorsFund", RawResponse = JsonConvert.SerializeObject(RawResponse), CreatedDate = DateTime.Now, CreatedBy = cardKnoxDonationRequest.UserId, ModifiedDate = DateTime.Now, ModifiedBy = cardKnoxDonationRequest.UserId, RawTransactionResponse = JsonConvert.SerializeObject(transaction) });
        //            transactionRawDataId = transactionRawData.TransactionRawDataId;
        //        }
        //        catch { }
        //        transactionId = await _organizationDonationRepository.AddOrganizationDonation(transaction, organizationDonationWithSavedCardViewModel.OrganizationPresetId, organizationDonationWithSavedCardViewModel.OrganizationPresetCategoryId, organizationDonationWithSavedCardViewModel.DonorName, organizationDonationWithSavedCardViewModel.HonorId, organizationDonationWithSavedCardViewModel.Note, organizationDonationWithSavedCardViewModel.NameOnCard, organizationDonationWithSavedCardViewModel.Email, organizationDonationWithSavedCardViewModel.IsOJC, organizationDonationWithSavedCardViewModel.Street, organizationDonationWithSavedCardViewModel.City, organizationDonationWithSavedCardViewModel.State, organizationDonationWithSavedCardViewModel.PostalCode, organizationDonationWithSavedCardViewModel.IsRecurring, organizationDonationWithSavedCardViewModel.Refrence, transactionRawDataId, cardKnoxDonationRequest.PhoneNumber);
        //        result.Status = transactionId > 0 && string.IsNullOrEmpty(transaction.Reason);
        //        if (!result.Status && string.IsNullOrEmpty(result.Message))
        //        {
        //            result.Message = transaction.Reason;
        //        }
        //        return (true);

        //}


        #endregion



        #region Private

        public async Task<bool> Payments(ChargeCardRequestDto cardKnoxDonationRequest)
        {
            bool status = true;
            Transactions transaction = new Transactions();
            CardknoxResponse cardknoxResponse;
            (transaction, cardknoxResponse) =await PaymentByCardknox( cardKnoxDonationRequest);
            return ( status);

        }

        private async Task<(Transactions, CardknoxResponse)> PaymentByCardknox(ChargeCardRequestDto cardKnoxDonationRequest)
        {
            try
            {
                var transactions = new Transactions();
                var response = _cardknoxPaymentService.PaymentByCreditCard(cardKnoxDonationRequest.AmountPerMonth, cardKnoxDonationRequest.CreditCardNumber, CommonHelper.GetStringValue(cardKnoxDonationRequest.ExpMonth), CommonHelper.GetStringValue(cardKnoxDonationRequest.ExpYear), cardKnoxDonationRequest.Cvv, _appSettings.ClientId, _appSettings.ClientSecret);
                bool isTransactionSucceeded = string.IsNullOrEmpty(response.Error);
                transactions.TransactionGuid = response.RefNum;
                transactions.CCProcessorId = (int)Domain.Enums.CCProcessorType.Cardknox;
                transactions.PaymentMethodId = (int)Domain.Enums.PaymentMethod.CreditCard;
                transactions.TransactionResult = isTransactionSucceeded ? Enum.GetName(typeof(Payment), Payment.succeeded) : Enum.GetName(typeof(Payment), Payment.failed);
                transactions.TransactionType = "Card";
                transactions.ItemTypeId = isTransactionSucceeded ? (int)Domain.Enums.ItemType.Success : (int)Domain.Enums.ItemType.Failed;
                transactions.Reason = isTransactionSucceeded ? null : $"Error : {response.Error}, ErrorCode : {response.ErrorCode}";
                var resultViewModel = await SendDonationEmail(cardKnoxDonationRequest, transactions.TransactionId, isTransactionSucceeded, response.Error);


                return (transactions, response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<ResultViewModel> SendDonationEmail (ChargeCardRequestDto cardKnoxDonationRequest, int transactionId, bool isTransactionSucceeded, string message)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            await _emailTemplateService.SendDonationMail(cardKnoxDonationRequest, new ResultViewModel { Status = (isTransactionSucceeded && transactionId > 0), Message = message });
            resultViewModel.Status = true;


            return resultViewModel;
        }


        #endregion
    }
}
