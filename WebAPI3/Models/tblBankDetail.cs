using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblBankDetail
    {
        public tblBankDetail()
        {
            this.tblStudentFeePaidHeaders = new List<tblStudentFeePaidHeader>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrgId { get; set; }
        public Nullable<bool> isBilling { get; set; }
        public virtual tblOrg tblOrg { get; set; }
        public virtual ICollection<tblStudentFeePaidHeader> tblStudentFeePaidHeaders { get; set; }
    }
}
