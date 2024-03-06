using CardknoxApi;
using CardknoxApi.Operations;
using Infrastructure.Attributes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DTO.Request;
using Common.Settings;
using Microsoft.Extensions.Options;
using Infrastructure.DTO.Request.Cardknox;
using Infrastructure.DTO.Response.Cardknox;
using System.Net.Http.Headers;
using DTO.Response;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Services.PaymentService;

namespace Infrastructure.Implementation.Services.PaymentService
{
    [ScopedService]
    public class CardknoxPaymentService : ICardknoxPaymentService
    {
        private const string contentType = "application/json-patch+json";
        private readonly AppSettings _appSettings;
        private readonly CardknoxSetting _cardknoxSetting;
        public CardknoxPaymentService(IOptions<AppSettings> appSettings, IOptions<CardknoxSetting> cardknoxSetting)
        {          
            _appSettings = appSettings.Value;
            _cardknoxSetting = cardknoxSetting.Value;
        }
        #region Method

        public CardknoxResponse PaymentByCreditCard(decimal amount, string cardNumber, string expirationMonth, string expirationYear, string cvv, string xkey, string clientSecret)
        {
            if (expirationYear.Length > 2)
            {
                expirationYear = expirationYear[2..];
            }
            CCSale cCSale = new CCSale
            {
                Amount = amount,
                CardNum = cardNumber,
                Exp = $"{expirationMonth}{expirationYear}",
                CVV = cvv,

               
            };

            CardknoxRequest cardknoxRequest = new CardknoxRequest(xkey, clientSecret, "1.0.1");
            Cardknox cardknox = new Cardknox(cardknoxRequest);                   
            var response = cardknox.CCSale(cCSale);
            return response;
        }

        public async Task<CardKnoxRecurringResponse> AddRecurringPayment(TransactionRequestDto transactionRequestDto)
        {
            HttpClient client = GetClient(_cardknoxSetting.Token).GetAwaiter().GetResult();
            CreateScheduleResponseDto createScheduleResponseDto = new CreateScheduleResponseDto();
            CardKnoxRecurringResponse cardKnoxRecurringResponse = new();
            CreateCustomerRequestDto createCustomerRequestDto = new()
            {
                SoftwareName = _cardknoxSetting.xSoftwareName,
                SoftwareVersion = _cardknoxSetting.xSoftwareVersion,
                BillPhone = transactionRequestDto.PhoneNumber,
                BillFirstName = transactionRequestDto.FirstName,
                BillLastName = transactionRequestDto.LastName,
                BillCity= transactionRequestDto.City,
                BillZip = transactionRequestDto.Zip,
                Email = transactionRequestDto.Email
            };

            //var createCustomerResponse = await CreateCustomers(createCustomerRequestDto, client);
            //if(createCustomerResponse.Error != null && createCustomerResponse.Error != "")
            //{
            //    cardKnoxRecurringResponse.IsError = true;
            //    cardKnoxRecurringResponse.ErrorMessage = createCustomerResponse.Error;
            //}
            //if (createCustomerResponse.Error == null || createCustomerResponse.Error == "")
            //{
            //    var cardKnoxPaymentResponse = SaveCreditCard(transactionRequestDto.Amount, transactionRequestDto.CreditCardNumber, transactionRequestDto.ExpMonth.ToString(), transactionRequestDto.ExpYear.ToString(), transactionRequestDto.Cvv, _cardknoxSetting.XKey, _cardknoxSetting.ClientSecret);
            //    if (cardKnoxPaymentResponse.Error != null && cardKnoxPaymentResponse.Error != "") 
            //    {
            //        cardKnoxRecurringResponse.IsError = true;
            //        cardKnoxRecurringResponse.ErrorMessage = cardKnoxPaymentResponse.Error;
            //    }


            //    if (createCustomerResponse != null && !string.IsNullOrEmpty(createCustomerResponse.CustomerId) && (cardKnoxPaymentResponse.Error == null || cardKnoxPaymentResponse.Error == ""))
            //    {
            //        CreatePaymentMethodRequestDto createPaymentMethodRequestDto = new()
            //        {
            //            SetAsDefault = true,
            //            Exp = transactionRequestDto.ExpDate,
            //            CustomerId = createCustomerResponse.CustomerId,
            //            SoftwareName = _cardknoxSetting.xSoftwareName,
            //            SoftwareVersion = _cardknoxSetting.xSoftwareVersion,
            //            Zip = transactionRequestDto.Zip,
            //            Token = cardKnoxPaymentResponse.Token,
            //            TokenType = "CC"
            //        };

            //        var createPaymentMethodResponse = await CreatePaymentMethod(createPaymentMethodRequestDto, client);
            //        if (createPaymentMethodResponse.Error != null && createPaymentMethodResponse.Error != "")
            //        {
            //            cardKnoxRecurringResponse.IsError = true;
            //            cardKnoxRecurringResponse.ErrorMessage = createPaymentMethodResponse.Error;
            //        }
            //        if (createPaymentMethodResponse != null && !string.IsNullOrEmpty(createPaymentMethodResponse.PaymentMethodId) && (createPaymentMethodResponse.Error == null || createPaymentMethodResponse.Error == ""))
            //        {
            //            CreateScheduleRequestDto createScheduleRequestDto = new()
            //            {
            //                Amount = transactionRequestDto.Amount,
            //                SoftwareName = _cardknoxSetting.xSoftwareName,
            //                SoftwareVersion = _cardknoxSetting.xSoftwareVersion,
            //                IntervalType = "Year",
            //                IntervalCount = 1,
            //                ScheduleName = "Samplee Schedule",
            //                TotalPayments = 5,
            //                SkipSaturdayAndHolidays = false,
            //                AllowInitialTransactionToDecline = false,
            //                CustReceipt = false,
            //                CustomerId = createCustomerResponse.CustomerId


            //            };
            //            createScheduleResponseDto = await CreateSchedule(createScheduleRequestDto, client);
            //            if (createScheduleResponseDto.Error != null && createScheduleResponseDto.Error != "")
            //            {
            //                cardKnoxRecurringResponse.IsError = true;
            //                cardKnoxRecurringResponse.ErrorMessage = createScheduleResponseDto.Error;
            //            }
            //        }

            //    }
            //}

            var createCustomerResponse = await CreateCustomers(createCustomerRequestDto, client);            
            if (createCustomerResponse != null && !string.IsNullOrEmpty(createCustomerResponse.CustomerId) && string.IsNullOrEmpty(createCustomerResponse.Error))
            {
                var cardKnoxPaymentResponse = SaveCreditCard(transactionRequestDto.Amount, transactionRequestDto.CreditCardNumber, transactionRequestDto.ExpMonth.ToString(), transactionRequestDto.ExpYear.ToString(), transactionRequestDto.Cvv, _cardknoxSetting.XKey, _cardknoxSetting.ClientSecret);                

                if (cardKnoxPaymentResponse!=null && string.IsNullOrEmpty(cardKnoxPaymentResponse.Error))
                {
                    CreatePaymentMethodRequestDto createPaymentMethodRequestDto = new()
                    {
                        SetAsDefault = true,
                        Exp = transactionRequestDto.ExpDate,
                        CustomerId = createCustomerResponse.CustomerId,
                        SoftwareName = _cardknoxSetting.xSoftwareName,
                        SoftwareVersion = _cardknoxSetting.xSoftwareVersion,
                        Zip = transactionRequestDto.Zip,
                        Token = cardKnoxPaymentResponse.Token,
                        TokenType = "CC"
                    };
                   
                    var createPaymentMethodResponse = await CreatePaymentMethod(createPaymentMethodRequestDto, client);                   
                    if (createPaymentMethodResponse != null && !string.IsNullOrEmpty(createPaymentMethodResponse.PaymentMethodId) && string.IsNullOrEmpty(createPaymentMethodResponse.Error))
                    {
                        CreateScheduleRequestDto createScheduleRequestDto = new()
                        {
                            Amount = transactionRequestDto.Amount,
                            SoftwareName = _cardknoxSetting.xSoftwareName,
                            SoftwareVersion = _cardknoxSetting.xSoftwareVersion,
                            IntervalType = "Year",
                            IntervalCount = 1,
                            ScheduleName = "Samplee Schedule",
                            TotalPayments = 5,
                            SkipSaturdayAndHolidays = false,
                            AllowInitialTransactionToDecline = false,
                            CustReceipt = false,
                            CustomerId = createCustomerResponse.CustomerId


                        };
                        createScheduleResponseDto = await CreateSchedule(createScheduleRequestDto, client);
                        if (!string.IsNullOrEmpty(createScheduleResponseDto.Error))
                        {
                            cardKnoxRecurringResponse.IsError = true;
                            cardKnoxRecurringResponse.ErrorMessage = createScheduleResponseDto!=null? createScheduleResponseDto.Error:" Something went wrong. Please Try again";
                        }
                    }
                    else
                    {
                        cardKnoxRecurringResponse.IsError = true;
                        cardKnoxRecurringResponse.ErrorMessage = createPaymentMethodResponse!=null? createPaymentMethodResponse.Error : " Something went wrong. Please Try again";
                    }

                }
                else
                {
                    cardKnoxRecurringResponse.IsError = true;
                    cardKnoxRecurringResponse.ErrorMessage = cardKnoxPaymentResponse!=null? cardKnoxPaymentResponse.Error : " Something went wrong. Please Try again";
                }
            }
            else
            {
                cardKnoxRecurringResponse.IsError = true;
                cardKnoxRecurringResponse.ErrorMessage = createCustomerResponse!=null? createCustomerResponse.Error : " Something went wrong. Please Try again";
            }

            return cardKnoxRecurringResponse;
        }

        private async Task<CreateCustomerResponseDto> CreateCustomers(CreateCustomerRequestDto createCustomerRequestDto, HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _cardknoxSetting.BaseUrl + "CreateCustomer")
            {
                Content = new StringContent(JsonConvert.SerializeObject(createCustomerRequestDto), Encoding.UTF8, contentType)
            };
            var result = await client.SendAsync(request).ConfigureAwait(false);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return result.StatusCode != HttpStatusCode.OK && result.StatusCode != HttpStatusCode.Created && result.StatusCode != HttpStatusCode.BadRequest
                ? throw new HttpRequestException(content)
                : JsonConvert.DeserializeObject<CreateCustomerResponseDto>(content);
         
        }

        private async Task<CreatePaymentMethodResponseDto> CreatePaymentMethod(CreatePaymentMethodRequestDto createPaymentMethodRequestDto, HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _cardknoxSetting.BaseUrl + "CreatePaymentMethod")
            {
                Content = new StringContent(JsonConvert.SerializeObject(createPaymentMethodRequestDto), Encoding.UTF8, contentType)
            };
            var result = await client.SendAsync(request).ConfigureAwait(false);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return result.StatusCode != HttpStatusCode.OK && result.StatusCode != HttpStatusCode.Created && result.StatusCode != HttpStatusCode.BadRequest
                ? throw new HttpRequestException(content)
                : JsonConvert.DeserializeObject<CreatePaymentMethodResponseDto>(content);
           
        }

        private async Task<CreateScheduleResponseDto> CreateSchedule(CreateScheduleRequestDto createScheduleRequestDto, HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _cardknoxSetting.BaseUrl + "CreateSchedule")
            {
                Content = new StringContent(JsonConvert.SerializeObject(createScheduleRequestDto), Encoding.UTF8, contentType)
            };
            var result = await client.SendAsync(request).ConfigureAwait(false);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return result.StatusCode != HttpStatusCode.OK && result.StatusCode != HttpStatusCode.Created && result.StatusCode != HttpStatusCode.BadRequest
                ? throw new HttpRequestException(content)
                : JsonConvert.DeserializeObject<CreateScheduleResponseDto>(content);
        
        }

        private Task<HttpClient> GetClient(string token)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://api.cardknox.com")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("X-Recurring-Api-Version", "2.1");
            return Task.FromResult(client);
        }

        public CardknoxResponse SaveCreditCard(decimal amount, string cardNumber, string expirationMonth, string expirationYear, string cvv, string xkey, string clientSecret)
        {
            if (expirationYear.Length > 2)
            {
                expirationYear = expirationYear[2..];
            }
            CCSave cCSave = new CCSave
            {
                Amount = amount,
                CardNum = cardNumber,
                Exp = $"{expirationMonth}{expirationYear}",
                CVV = cvv,
            };
            CardknoxRequest cardknoxRequest = new CardknoxRequest(xkey, clientSecret, "1.0.1");
            Cardknox cardknox = new Cardknox(cardknoxRequest);
            var response = cardknox.CCSave(cCSave);
            return response;
        }
        #endregion
    }
}
