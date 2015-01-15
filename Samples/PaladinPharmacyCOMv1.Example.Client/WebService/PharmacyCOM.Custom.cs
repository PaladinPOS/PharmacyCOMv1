using System;
using System.Collections.Generic;
using System.Text;

namespace PaladinPharmacyCOMv1.Example.Client.WebService
{
    public partial class PharmacyCOM
    {

        /// <summary>
        /// Creates new instance of a PharmacyCOM web service interface.
        /// </summary>
        /// <param name="url">URL of webservice to connect to.</param>
        /// <remarks>Custom constructor to allow user configuration of webservice URL.</remarks>
        public PharmacyCOM(string url)
        {
            this.Url = url;
        }

    }
}
