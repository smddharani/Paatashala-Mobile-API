using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblRetail_InvoiceDetail
    {
        public long Id { get; set; }
        public Nullable<long> ItemCode { get; set; }
        public string ItemName { get; set; }
        public Nullable<long> Quantity { get; set; }
        public Nullable<long> Unit { get; set; }
        public Nullable<decimal> RatePerUnit { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public long OrgId { get; set; }
    }
}
