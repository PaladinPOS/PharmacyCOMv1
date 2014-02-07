using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.Serialization;

namespace PaladinPharmacyCOMv1.Models
{
    public class RxItem
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        private string m_rxNumber;
        private string m_rxCustomerName;
        private bool m_rxValid;
        private decimal m_rxAmtDue;
        private bool m_rxTaxable;
        private List<RxMessage> m_rxMessages = new List<RxMessage>();
        private List<RxItemFlag> m_rxItemFlags = new List<RxItemFlag>();

        //2010.08.06 - KH: New Fields added to service for customer data
        private string m_rxCustomerFName;
        private string m_rxCustomerMName;
        private string m_rxCustomerLName;
        private string m_rxPatientID;
        private string m_rxCustomerPhone1;
        private string m_rxCustomerRegAddress1;
        private string m_rxCustomerRegAddress2;
        private string m_rxCustomerRegCity;
        private string m_rxCustomerRegState;
        private string m_rxCustomerRegZIP;

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public string RxNumber
        {
            get { return m_rxNumber; }
            set { m_rxNumber = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public bool RxValid
        {
            get { return m_rxValid; }
            set { m_rxValid = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public string RxCustomerName
        {
            get { return m_rxCustomerName; }
            set { m_rxCustomerName = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerFName
        {
            get { return m_rxCustomerFName; }
            set { m_rxCustomerFName = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerMName
        {
            get { return m_rxCustomerMName; }
            set { m_rxCustomerMName = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerLName
        {
            get { return m_rxCustomerLName; }
            set { m_rxCustomerLName = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxPatientID
        {
            get { return m_rxPatientID; }
            set { m_rxPatientID = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerPhone1
        {
            get { return m_rxCustomerPhone1; }
            set { m_rxCustomerPhone1 = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerRegAddress1
        {
            get { return m_rxCustomerRegAddress1; }
            set { m_rxCustomerRegAddress1 = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerRegAddress2
        {
            get { return m_rxCustomerRegAddress2; }
            set { m_rxCustomerRegAddress2 = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerRegCity
        {
            get { return m_rxCustomerRegCity; }
            set { m_rxCustomerRegCity = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerRegState
        {
            get { return m_rxCustomerRegState; }
            set { m_rxCustomerRegState = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        /// <remarks/>
        public string RxCustomerRegZIP
        {
            get { return m_rxCustomerRegZIP; }
            set { m_rxCustomerRegZIP = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public decimal RxAmountDue
        {
            get { return m_rxAmtDue; }
            set { m_rxAmtDue = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public bool RxTaxable
        {
            get { return m_rxTaxable; }
            set { m_rxTaxable = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public List<RxMessage> RxMessages
        {
            get { return m_rxMessages; }
            set { m_rxMessages = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public List<RxItemFlag> RxItemFlags
        {
            get { return m_rxItemFlags; }
            set { m_rxItemFlags = value; }
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}
