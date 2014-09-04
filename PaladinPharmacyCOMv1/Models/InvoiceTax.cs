using System;
using System.Collections.Generic;
using System.Web;

namespace PaladinPharmacyCOMv1.Models
{
    public class InvoiceTax
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int? Categories { get; set; }
        public string Expansion { get; set; }
    }
}