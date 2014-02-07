using System;
using System.Collections.Generic;
using System.Web;

namespace PaladinPharmacyCOMv1.Models
{
    public class InvoiceText
    {
        public int InvoiceId { get; set; }
        public int LineNumber { get; set; }
        public string Value { get; set; }
        public int? Categories { get; set; }
        public string Expansion { get; set; }
    }
}