using PaladinPharmacyCOMv1.Example.Services;
using PaladinPharmacyCOMv1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PaladinPharmacyCOMv1.Example
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PharmacyCOM" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PharmacyCOM.svc or PharmacyCOM.svc.cs at the Solution Explorer and start debugging.
    [XmlSerializerFormat]
    [ServiceContract(Namespace = "http://services.paladinpos.com/PaladinPharmacyCOMv1")]
    public class PharmacyCOM_WCF
    {
        private static DemoPharmacyService service = new DemoPharmacyService();

        [OperationContract(Name = "GetAvailableCredit", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/GetAvailableCredit")]
        public AvailableCreditResponse GetAvailableCredit(string customerId)
        {
            return service.GetAvailableCredit(customerId);
        }

        [OperationContract(Name = "GetRxItem", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/GetRxItem")]
        public RxItem GetRxItem(string rxNumber)
        {
            return service.GetRxItem(rxNumber);
        }

        [OperationContract(Name = "SaveInvoice", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/SaveInvoice")]
        public bool SaveInvoice(Invoice invoice)
        {
            return service.SaveInvoice(invoice);
        }

        [OperationContract(Name = "SaveRxInvoice", Action = @"http://services.paladinpos.com/PaladinPharmacyCOMv1/SaveRxInvoice")]
        public bool SaveRxInvoice(RxInvoice rxInvoice)
        {
            return service.SaveRxInvoice(rxInvoice);
        }
    }
}
