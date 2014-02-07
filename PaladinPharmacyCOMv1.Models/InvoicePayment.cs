using System;
using System.Collections.Generic;
using System.Web;

namespace PaladinPharmacyCOMv1.Models
{
    public class InvoicePayment
    {
        public int InvoiceId { get; set; }
        public int LineNumber { get; set; }
        public DateTime? Date { get; set; }
        public int? Type { get; set; }
        public decimal? Amount { get; set; }
        public string Data { get; set; }
        public int? SaleId { get; set; }
        public int? Categories { get; set; }
        public string Expansion { get; set; }
    }
}