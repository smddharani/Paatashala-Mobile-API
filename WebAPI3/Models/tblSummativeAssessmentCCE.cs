using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblSummativeAssessmentCCE
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long TermId { get; set; }
        public long OrgId { get; set; }
        public virtual tblOrg tblOrg { get; set; }
        public virtual tblTermCCE tblTermCCE { get; set; }
    }
}
