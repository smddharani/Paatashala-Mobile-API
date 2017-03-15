using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblExpenseCategory
    {
        public tblExpenseCategory()
        {
            this.tblExpenseDetails = new List<tblExpenseDetail>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<long> OrgId { get; set; }
        public virtual tblOrg tblOrg { get; set; }
        public virtual ICollection<tblExpenseDetail> tblExpenseDetails { get; set; }
    }
}
