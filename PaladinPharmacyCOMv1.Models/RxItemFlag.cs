using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Serialization;


namespace PaladinPharmacyCOMv1.Models
{
    public class RxItemFlag
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        private int m_type;
        private string m_message;
        private bool m_required;

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        [XmlAttribute()]
        public int Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        //---------------------------------------------------------------------------------------------------------


        [XmlAttribute()]
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }

        //---------------------------------------------------------------------------------------------------------

        [XmlAttribute()]
        public bool Required
        {
            get { return m_required; }
            set { m_required = value; }
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public RxItemFlag() {}

        public RxItemFlag(int flagType, string flagMsg)
        {
            m_type = flagType;
            m_message = flagMsg;
        }

        public RxItemFlag(int flagType, string flagMsg, bool required) : this(flagType, flagMsg)
        {
            m_required = required;
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

    }
}
