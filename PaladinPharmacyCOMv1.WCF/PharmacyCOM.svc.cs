using PaladinPharmacyCOMv1.Interfaces;
using PaladinPharmacyCOMv1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;

namespace PaladinPharmacyCOMv1.WCF
{
    [XmlSerializerFormat]
    [ServiceContract(Namespace = "http://services.paladinpos.com/PaladinPharmacyCOMv1")]
    public class PharmacyCOM : IPharmacyCOMv1Service
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
        [OperationContract(Name = "GetRxItem", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/GetRxItem")]
        public RxItem GetRxItem(string rxNumber)
        {
            //TODO: Get rxNumber from pharmacy system and return as RxItem to Paladin POS.
            throw new NotImplementedException("Method to be implemented by pharmacy system.");
        }

        //---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get RxItem info from pharmacy system.
        /// </summary>
        /// <param name="rxNumber"></param>
        /// <returns>
        /// Instance of <see cref="RxItem"/> containing details about the requested partnumber.
        /// </returns>
        /// <remarks>
        ///     Paladin POS will call this method when requesting a perscription from the pharmacy system if 
        ///     return multiple items is enabled.
        /// </remarks>
        [OperationContract(Name = "GetRxItems", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/GetRxItems")]
        public List<RxItem> GetRxItems(string rxNumber)
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
        [OperationContract(Name = "SaveRxInvoice", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/SaveRxInvoice")]
        public bool SaveRxInvoice(Models.RxInvoice rxInvoice)
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
        [OperationContract(Name = "GetAvailableCredit", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/GetAvailableCredit")]
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
        [OperationContract(Name = "SaveInvoice", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/SaveInvoice")]
        public bool SaveInvoice(Models.Invoice invoice)
        {
            //TODO: Save Invoice to pharmacy system and return save status to Paladin POS.
            throw new NotImplementedException("Method to be implemented by pharmacy system.");
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}
