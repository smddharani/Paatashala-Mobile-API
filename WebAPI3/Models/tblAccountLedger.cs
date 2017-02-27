using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblAccountLedger
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AccountGroup { get; set; }
        public long OrgId { get; set; }
        public virtual tblAccountGroup tblAccountGroup { get; set; }
        public virtual tblOrg tblOrg { get; set; }
    }
}
