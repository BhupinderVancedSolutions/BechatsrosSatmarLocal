using Application.Common.Interfaces.Services.PaymentService;
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

        public CardknoxResponse PaymentByCreditCard(decimal amount, string cardNumber, string expirationMonth, string expirationYear, string cvv, string clientId, string clientSecret)
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

            CardknoxRequest cardknoxRequest = new CardknoxRequest(clientId, clientSecret, "1.0.1");
            Cardknox cardknox = new Cardknox(cardknoxRequest);                   
            return cardknox.CCSale(cCSale);
        }

        public async Task<CreateScheduleResponseDto> AddRecurringPayment(TransactionRequestDto cardKnoxDonationRequest)
        {
            HttpClient client = GetClient(_cardknoxSetting.Token).GetAwaiter().GetResult();
            CreateScheduleResponseDto createScheduleResponseDto =new CreateScheduleResponseDto();

            CreateCustomerRequestDto createCustomerRequestDto = new CreateCustomerRequestDto();
            CreatePaymentMethodRequestDto createPaymentMethodRequestDto = new CreatePaymentMethodRequestDto();
            CreateScheduleRequestDto createScheduleRequestDto = new CreateScheduleRequestDto();
            var createCustomerResponse = await CreateCustomers(createCustomerRequestDto, client);
            if (createCustomerResponse != null && !string.IsNullOrEmpty(createCustomerResponse.CustomerId))
            {
                var createPaymentMethodResponse = await CreatePaymentMethod(createPaymentMethodRequestDto, client);
                if (createPaymentMethodResponse != null && !string.IsNullOrEmpty(createPaymentMethodResponse.PaymentMethodId))
                {
                    createScheduleResponseDto = await CreateSchedule(createScheduleRequestDto, client);
                }

            }
            return createScheduleResponseDto;
        }

        private async Task<CreateCustomerResponseDto> CreateCustomers(CreateCustomerRequestDto createCustomerRequestDto, HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.cardknox.com/v2/CreateCustomer")
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
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.cardknox.com/v2/CreatePaymentMethod")
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
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.cardknox.com/v2/CreateSchedule")
            {
                Content = new StringContent(JsonConvert.SerializeObject(createScheduleRequestDto), Encoding.UTF8, contentType)
            };
            var result = await client.SendAsync(request).ConfigureAwait(false);
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            return result.StatusCode != HttpStatusCode.OK && result.StatusCode != HttpStatusCode.Created && result.StatusCode != HttpStatusCode.BadRequest
                ? throw new HttpRequestException(content)
                : JsonConvert.DeserializeObject<CreateScheduleResponseDto>(content);
        }
        private async Task<HttpClient> GetClient(string token)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://api.cardknox.com")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            client.DefaultRequestHeaders.Add("Authorization", token);
            return client;
        }
        #endregion
    }
}
