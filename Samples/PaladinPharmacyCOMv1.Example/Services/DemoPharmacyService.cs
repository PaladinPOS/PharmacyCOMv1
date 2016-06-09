using PaladinPharmacyCOMv1.Interfaces;
using PaladinPharmacyCOMv1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PaladinPharmacyCOMv1.Example.Services
{
    public class DemoPharmacyService : IPharmacyCOMv1Service
    {
        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public RxItem GetRxItem(string rxNumber)
        {
            //Simulate GetRxItem from a pharmacy system
            RxItem item = null;

            if (rxNumber.ToUpper() == "RXFAMILYCHECKOUT1") { item = CreateDummyRxItem_rxFamilyCheckout1(); }
            else if (rxNumber.ToUpper() == "RXFAMILYCHECKOUT2") { item = CreateDummyRxItem_rxFamilyCheckout2(); }
            else { item = CreateDummyRxItem_RandomRxNumber(rxNumber); }

            return item;
        }

        //---------------------------------------------------------------------------------------------------------

        public List<RxItem> GetRxItems(string rxNumber)
        {
            //Simulate GetRxItem from a pharmacy system
            List<RxItem> items = new List<RxItem>();

            if (rxNumber.ToUpper() == "RXFAMILYCHECKOUT1") { items.Add(CreateDummyRxItem_rxFamilyCheckout1()); }
            else if (rxNumber.ToUpper() == "RXFAMILYCHECKOUT2") { items.Add(CreateDummyRxItem_rxFamilyCheckout2()); }
            else { items.Add(CreateDummyRxItem_RandomRxNumber(rxNumber)); }

            items.Add(CreateDummyRxItem_RandomRxNumber(rxNumber + "-2"));

            return items;
        }

        //---------------------------------------------------------------------------------------------------------

        private RxItem CreateDummyRxItem_RandomRxNumber(string rxNumber)
        {
            RxItem item = new RxItem();
            item.RxNumber = rxNumber;
            item.RxValid = true;
            item.RxCustomerName = "Test Customer";
            item.RxPatientID = "111222333444555666";
            item.RxAmountDue = 44.44m;
            item.RxTaxable = true;

            item.RxCustomerRegAddress1 = "321 Bogus Ave.";
            item.RxCustomerRegCity = "HomeTown";
            item.RxCustomerRegState = "OK";
            item.RxCustomerRegZIP = "34567";

            ////test new fields for v1.07
            //item.RxCustomerPhone1 = "5411234567";
            //item.RxCustomerPhone2 = "5419876543";
            //item.RxCustomerEmail1 = "email1@paladinpos.com";
            //item.RxCustomerEmail2 = "email2@paladinpos.com";
            //item.RxCustomerDeliveryAddress1 = "54321 Main St.";
            //item.RxCustomerDeliveryAddress2 = "ATTN: Billy";
            //item.RxCustomerDeliveryCity = "Dallas";
            //item.RxCustomerDeliveryState = "TX";
            //item.RxCustomerDeliveryZIP = "12345";
            
            item.RxMessages.Add(new RxMessage("This item is a screen only message"));
            item.RxMessages.Add(new RxMessage("This message is for everyone!", true));

            item.RxItemFlags.Add(new RxItemFlag(1, "HIPPA First Time Signature", false));
            item.RxItemFlags.Add(new RxItemFlag(2, "No Safety Cap on Rx", true));
            item.RxItemFlags.Add(new RxItemFlag(3, "Need Pharmacist Consult", true));

            return item;
        }

        //---------------------------------------------------------------------------------------------------------

        private RxItem CreateDummyRxItem_rxFamilyCheckout1()
        {
            RxItem item = new RxItem();
            item.RxNumber = "rxFamilyCheckout1";
            item.RxValid = true;
            item.RxCustomerName = "Joe Bob";
            item.RxPatientID = "998877665544332210";
            item.RxAmountDue = 50.00m;
            item.RxTaxable = true;

            item.RxMessages.Add(new RxMessage("This item is a screen only message"));
            item.RxMessages.Add(new RxMessage("This message is for everyone!", true));

            item.RxItemFlags.Add(new RxItemFlag(1, "HIPPA First Time Signature", false));
            item.RxItemFlags.Add(new RxItemFlag(2, "No Safety Cap on Rx", true));
            item.RxItemFlags.Add(new RxItemFlag(3, "Need Pharmacist Consult", true));

            return item;
        }

        //---------------------------------------------------------------------------------------------------------

        private RxItem CreateDummyRxItem_rxFamilyCheckout2()
        {
            RxItem item = new RxItem();
            item.RxNumber = "rxFamilyCheckout2";
            item.RxValid = true;
            item.RxCustomerName = "Billy-Jean Bob";
            item.RxPatientID = "998877665544332211";
            item.RxAmountDue = 50.00m;
            item.RxTaxable = true;

            item.RxMessages.Add(new RxMessage("This item is a screen only message"));
            item.RxMessages.Add(new RxMessage("This message is for everyone!", true));

            item.RxItemFlags.Add(new RxItemFlag(1, "HIPPA First Time Signature", false));
            item.RxItemFlags.Add(new RxItemFlag(2, "No Safety Cap on Rx", true));
            item.RxItemFlags.Add(new RxItemFlag(3, "Need Pharmacist Consult", true));

            return item;
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public bool SaveRxInvoice(RxInvoice rxInvoice)
        {
            //Simulate pharmacy system saved rx invoice successfully. 
            return true;
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public AvailableCreditResponse GetAvailableCredit(string customerId)
        {
            //Simulate get available credit from pharmacy system
            AvailableCreditResponse response = null;
            switch (customerId)
            {
                case "111222333444555666":
                    response = CreateValidAvailibleCreditResponse(customerId);
                    break;
                default:
                    response = CreateInvalidAvailibleCreditResponse(customerId);
                    break;
            }

            //Return result
            return response;
        }

        //---------------------------------------------------------------------------------------------------------

        private AvailableCreditResponse CreateValidAvailibleCreditResponse(string customerId)
        {
            Random random = new Random();
            AvailableCreditResponse response = new AvailableCreditResponse();
            response.IsValid = true;
            response.CustomerId = customerId;
            response.AvailableCredit = Math.Round(Convert.ToDecimal(random.Next(500) + random.NextDouble()), 2);
            response.Message = "success";
            response.AccountBalance = 999.99m;

            return response;
        }

        //---------------------------------------------------------------------------------------------------------

        private AvailableCreditResponse CreateInvalidAvailibleCreditResponse(string customerId)
        {
            Random random = new Random();
            AvailableCreditResponse response = new AvailableCreditResponse();
            response.IsValid = false;
            response.CustomerId = customerId;
            response.Message = "Customer not found";

            return response;
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------

        public bool SaveInvoice(Invoice invoice)
        {
            //Simulate pharmacy system saved invoice successfully. 
            return true;
        }

        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
    }
}