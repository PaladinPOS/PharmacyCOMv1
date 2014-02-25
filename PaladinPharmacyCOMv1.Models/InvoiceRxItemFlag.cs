using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Serialization;

namespace PaladinPharmacyCOMv1.Models
{
    public class InvoiceRxItemFlag
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? Type { get; set; }
        public bool? Accepted { get; set; }
        public byte[] Signature { get; set; }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public InvoiceRxItemFlag() { }

        public InvoiceRxItemFlag(string customerName, int flagType)
        {
            CustomerName = customerName;
            Type = flagType;
        }

        public InvoiceRxItemFlag(string customerName, int flagType, bool accepted)
            : this(customerName, flagType)
        {
            Accepted = accepted;
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}
