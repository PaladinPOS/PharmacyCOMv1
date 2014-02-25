using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.Serialization;


namespace PaladinPharmacyCOMv1.Models
{

    public class Invoice
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public int Id { get; set; }
        public bool? Deleted { get; set; }
        public int? Number { get; set; }
        public DateTime? Date { get; set; }
        public int? AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string PharmacyAccountNumber { get; set; }
        public int? AccountTracker { get; set; }
        public int? TerminalNumber { get; set; }
        public int? EmployeeNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public decimal? Total { get; set; }
        public decimal? Profit { get; set; }
        public decimal? PromptPaymentDiscount { get; set; }
        public bool? GlobalTax { get; set; }
        public bool? GlobalNet { get; set; }
        public bool? GlobalDefective { get; set; }
        public bool? DefaultTaxOverride { get; set; }
        public int? InvoiceType { get; set; }
        public byte[] Signature { get; set; }
        public string Expansion { get; set; }
        public decimal? FlexTotal { get; set; }
        public int? StoreId { get; set; }
        public string CustomerRewardNumber { get; set; }
        public int? Categories { get; set; }

        //---------------------------------------------------------------------------------------------------------

        public List<InvoiceItem> InvoiceItems { get; set; }

        //---------------------------------------------------------------------------------------------------------

        public List<InvoiceText> InvoiceText { get; set; }

        //---------------------------------------------------------------------------------------------------------

        public List<InvoicePayment> InvoicePayments { get; set; }

        //---------------------------------------------------------------------------------------------------------

        public List<InvoiceTax> InvoiceTaxes { get; set; }

        //---------------------------------------------------------------------------------------------------------

        public List<InvoiceRxItemFlag> InvoiceRxItemFlags { get; set; }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}
