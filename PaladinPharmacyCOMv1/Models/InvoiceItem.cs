using System;
using System.Collections.Generic;
using System.Web;

namespace PaladinPharmacyCOMv1.Models
{
    public class InvoiceItem
    {
        public int InvoiceId { get; set; }
        public int LineNumber { get; set; }
        public int? InventoryId { get; set; }
        public DateTime? Date { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string PrimaryNumber { get; set; }
        public int? DepartmentId { get; set; }
        public decimal? QuantitySold { get; set; }
        public decimal? ReorderQuantity { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? SoldPrice { get; set; }
        public int? SellThreshold { get; set; }
        public bool? Taxable { get; set; }
        public bool? Net { get; set; }
        public bool? Defective { get; set; }
        public decimal? LineExt { get; set; }
        public decimal? PromptPaymentDiscount { get; set; }
        public decimal? Profit { get; set; }
        public decimal? CostPerUnit { get; set; }
        public decimal? CostLineExt { get; set; }
        public int? PriceType { get; set; }
        public int? PricingPlanId { get; set; }
        public int? SaleId { get; set; }
        public int? SupplierId1 { get; set; }
        public int? SupplierId2 { get; set; }
        public int? ClassId1 { get; set; }
        public int? SubclassId1 { get; set; }
        public int? ClassId2 { get; set; }
        public int? SubclassId2 { get; set; }
        public int? ClassId3 { get; set; }
        public int? SubclassId3 { get; set; }
        public string Expansion { get; set; }
        public bool? FlexItem { get; set; }
        public int? StoreId { get; set; }
        public int? Categories { get; set; }
        public bool? RxItem { get; set; }
    }
}