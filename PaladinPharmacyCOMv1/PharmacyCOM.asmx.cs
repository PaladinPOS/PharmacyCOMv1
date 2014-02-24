using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using PaladinPharmacyCOMv1.Models;

namespace PaladinPharmacyCOMv1
{
    /// <summary>
    /// Pharmacy Communication interface for Paladin POS.
    /// </summary>
    [WebService(Namespace = "http://services.paladinpos.com/PaladinPharmacyCOMv1")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class PharmacyCOM : System.Web.Services.WebService
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get RxItem info from pharmacy system.
        /// </summary>
        /// <param name="rxNumber"></param>
        /// <returns>
        /// Instance of <see cref="RxItem"/> containing details about the requested partnumber.
        /// </returns>
        /// <remarks>
        ///     Paladin POS will call this method when requesting a perscription from the pharmacy system.
        /// </remarks>
        [WebMethod]
        public RxItem GetRxItem(string rxNumber)
        {
            //TODO: Get rxNumber from pharmacy system and return as RxItem to Paladin POS.
            throw new NotImplementedException("Method to be implemented by pharmacy system.");
        }

        //---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Save RxInvoice to pharmacy system. 
        /// </summary>
        /// <param name="rxInvoice"></param>
        /// <returns><c>True</c> if <see cref="RxInvoice"/> saved successfully, <c>False</c> otherwise.</returns>
        /// <remarks>
        ///     Paladin POS will call this method for any invoices that contain <see cref="RxItem"/>s.
        /// </remarks>
        [WebMethod]
        public bool SaveRxInvoice(RxInvoice rxInvoice)
        {
            //TODO: Save rxInvoice to pharmacy system and return save status to Paladin POS.
            throw new NotImplementedException("Method to be implemented by pharmacy system.");
        }

        //---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get a customer's available credit from pharmacy system.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <remarks>
        ///     Added in Pharmacy Spec v1.03.
        ///     Paladin POS must be configured to call this method -- method does not get called by default.
        ///     
        ///     An AvailableCredit value of zero or less will disable customer account charges in Paladin POS.
        /// </remarks>
        [WebMethod]
        public AvailableCreditResponse GetAvailableCredit(string customerId)
        {
            //TODO: Get available credit from pharmacy system and return as AvailableCreditResponse to Paladin POS.
            throw new NotImplementedException("Method to be implemented by pharmacy system.");
        }

        //---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Save Paladin Invoice to pharmacy system.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns><c>True</c> if <see cref="RxInvoice"/> saved successfully, <c>False</c> otherwise.</returns>
        /// <remarks>
        ///     Added in Pharmacy Spec v1.03.
        ///     Paladin POS must be configured to call this method* -- method does not get called by default.
        ///         * If enabled in Paladin POS, all calls to <see cref="SaveRxInvoice"/> method will be disabled.
        /// </remarks>
        [WebMethod]
        public bool SaveInvoice(Invoice invoice)
        {
            //TODO: Save Invoice to pharmacy system and return save status to Paladin POS.
            throw new NotImplementedException("Method to be implemented by pharmacy system.");
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}
