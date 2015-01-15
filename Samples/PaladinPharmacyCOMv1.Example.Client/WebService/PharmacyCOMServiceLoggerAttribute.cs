using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;

namespace PaladinPharmacyCOMv1.Example.Client.WebService
{
    /// <summary>
    /// PharmacyCOMServiceLogger soap extention attribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PharmacyCOMServiceLoggerAttribute : SoapExtensionAttribute
    {
        //..............................................................................................................
        //..............................................................................................................

        private int m_priority = 1;

        //..............................................................................................................
        //..............................................................................................................

        public override Type ExtensionType
        {
            get { return typeof(PharmacyCOMServiceLogger); }
        }

        //..............................................................................................................

        public override int Priority
        {
            get { return m_priority; }
            set { m_priority = value; }
        }

        //..............................................................................................................
        //..............................................................................................................
    }
}
