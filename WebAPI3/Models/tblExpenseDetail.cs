using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblExpenseDetail
    {
        public long Id { get; set; }
        public string ExpenseName { get; set; }
        public Nullable<long> Category { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<long> OrgId { get; set; }
        public virtual tblExpenseCategory tblExpenseCategory { get; set; }
        public virtual tblOrg tblOrg { get; set; }
    }
}
