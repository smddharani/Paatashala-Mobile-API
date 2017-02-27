using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblDiscountType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long OrgId { get; set; }
        public virtual tblOrg tblOrg { get; set; }
    }
}
