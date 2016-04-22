using PaladinPharmacyCOMv1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PaladinPharmacyCOMv1.Interfaces
{
    public interface IPharmacyCOMv1Service
    {
        AvailableCreditResponse GetAvailableCredit(string customerId);

        RxItem GetRxItem(string rxNumber);

        List<RxItem> GetRxItems(string rxNumber);

        bool SaveInvoice(Invoice invoice);

        bool SaveRxInvoice(RxInvoice rxInvoice);
    }
}
