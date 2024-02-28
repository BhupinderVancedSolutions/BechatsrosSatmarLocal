using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Request
{
    public class TransactionRequest
    {
        public string TransactionType { get; set; }
        public string EntryMode { get; set; }
        public string Token { get; set; }
        public CardData CardData { get; set; }
        public InvoiceData InvoiceData { get; set; }
    }

    public class CardData
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string TransactionType { get; set; }
        public string CVV { get; set; }
        public string ZipCode { get; set; }
        public string NameOnCard { get; set; }
        public string Street { get; set; }
        public string TrackData { get; set; }
        public Presentation Presentation { get; set; }

    }
    public class Presentation
    {
        public bool IsCardPresent { get; set; }
    }
    public class BINData
    {
        public int BIN { get; set; }
        public string CardBrand { get; set; }
        public string IssuingOrg { get; set; }
        public string CardType { get; set; }
        public string CardCategory { get; set; }
        public string IssuingCountry { get; set; }
        public string IssuingCountryCodeA2 { get; set; }
        public string IssuingCountryCodeA3 { get; set; }
        public int IssuingCountryNumber { get; set; }
        public string IssuingPhone { get; set; }
        public string IssuingWebsite { get; set; }
        public string PanLength { get; set; }
        public string IssuedEntity { get; set; }
        public string IsRegulated { get; set; }
        public bool IsCommercial { get; set; }

    }
    public class CardValidationData
    {
        public string AVSResponse { get; set; }
        public string CVResponse { get; set; }
        public string AVSResponseText { get; set; }
        public string CVResultText { get; set; }
        public string StreetMatchText { get; set; }
        public string ZipMatchText { get; set; }

    }
    public class CardDetails
    {
        public decimal AuthorizedAmount { get; set; }
        public string AuthorizationCode { get; set; }
        public string EntryMode { get; set; }
        public CardData CardData { get; set; }
        public BINData BINData { get; set; }
        public CardValidationData CardValidationData { get; set; }

    }
    public class InvoiceData
    {
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TipAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public decimal DutyAmount { get; set; }
        public decimal ConvenienceAmount { get; set; }
        public decimal SurchargeAmount { get; set; }
        public decimal CashBackAmount { get; set; }
    }
}
