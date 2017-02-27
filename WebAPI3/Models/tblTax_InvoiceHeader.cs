using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblTax_InvoiceHeader
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string PONumber { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public Nullable<long> Customer { get; set; }
        public string Address { get; set; }
        public Nullable<long> TinOrCst { get; set; }
        public Nullable<long> TaxType { get; set; }
        public Nullable<long> TradeDiscountPercent { get; set; }
        public Nullable<long> SpecialDiscountPercent { get; set; }
        public Nullable<long> TotalTax { get; set; }
    }
}
