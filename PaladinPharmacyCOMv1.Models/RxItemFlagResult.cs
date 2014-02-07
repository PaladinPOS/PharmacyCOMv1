using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Serialization;

namespace PaladinPharmacyCOMv1.Models
{
    public class RxItemFlagResult 
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        private string m_customerName;
        private int m_type;
        private bool m_accepted;
        private byte[] m_signature; 

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        [XmlAttribute()]
        public string CustomerName 
        {
            get { return m_customerName; }
            set { m_customerName = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        [XmlAttribute()]
        public int Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        [XmlAttribute()]
        public bool Accepted
        {
            get { return m_accepted; }
            set { m_accepted = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        public byte[] Signature
        {
            get { return m_signature; }
            set { m_signature = value; }
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public RxItemFlagResult() { }

        public RxItemFlagResult(string customerName, int flagType)
        {
            m_customerName = customerName;
            m_type = flagType;
        }

        public RxItemFlagResult(string customerName, int flagType, bool accepted)
            : this(customerName, flagType)
        {
            m_accepted = accepted;
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}
