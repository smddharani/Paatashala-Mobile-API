using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblStudentCustomize
    {
        public long Id { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public long OrgId { get; set; }
        public virtual tblOrg tblOrg { get; set; }
    }
}
