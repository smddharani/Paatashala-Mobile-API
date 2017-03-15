using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblFeePaidDetail
    {
        public long Id { get; set; }
        public long FeePaidHeader { get; set; }
        public long FeeItemId { get; set; }
        public decimal Amount { get; set; }
        public long OrgId { get; set; }
        public Nullable<bool> isBilling { get; set; }
        public virtual tblFeeItemDetail tblFeeItemDetail { get; set; }
        public virtual tblOrg tblOrg { get; set; }
        public virtual tblStudentFeePaidHeader tblStudentFeePaidHeader { get; set; }
    }
}
