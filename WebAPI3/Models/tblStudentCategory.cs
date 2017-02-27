using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblStudentCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrgId { get; set; }
        public virtual tblOrg tblOrg { get; set; }
    }
}
