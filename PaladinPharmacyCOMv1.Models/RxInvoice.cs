using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.Serialization;

namespace PaladinPharmacyCOMv1.Models
{

    public class RxInvoice
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        private int m_rxInvoiceNumber;
        private DateTime m_rxInvoiceDate;
        private decimal m_rxSubTotal;
        private decimal m_rxTaxTotal;
        private decimal m_rxTotal;
        private List<RxItem> m_rxItems = new List<RxItem>();
        private List<RxPayment> m_rxPayments = new List<RxPayment>();
        private List<RxItemFlagResult> m_rxItemFlagResults = new List<RxItemFlagResult>();

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public int RxInvoiceNumber
        {
            get { return m_rxInvoiceNumber; }
            set { m_rxInvoiceNumber = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public DateTime RxInvoiceDate
        {
            get { return m_rxInvoiceDate; }
            set { m_rxInvoiceDate = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public decimal RxSubTotal
        {
            get { return m_rxSubTotal; }
            set { m_rxSubTotal = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public decimal RxTaxTotal
        {
            get { return m_rxTaxTotal; }
            set { m_rxTaxTotal = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public decimal RxTotal
        {
            get { return m_rxTotal; }
            set { m_rxTotal = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public List<RxItem> RxItems
        {
            get { return m_rxItems; }
            set { m_rxItems = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public List<RxPayment> RxPayments
        {
            get { return m_rxPayments; }
            set { m_rxPayments = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public List<RxItemFlagResult> RxItemFlagResults
        {
            get { return m_rxItemFlagResults; }
            set { m_rxItemFlagResults = value; }
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}
