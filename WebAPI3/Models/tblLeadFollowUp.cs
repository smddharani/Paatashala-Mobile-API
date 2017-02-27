using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblLeadFollowUp
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> AssignTo { get; set; }
        public string Explanation { get; set; }
        public Nullable<System.DateTime> FollowupTime { get; set; }
        public string Channel { get; set; }
        public string ChannelInfo { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsDismissed { get; set; }
        public Nullable<long> OrgId { get; set; }
        public virtual tblEmployee tblEmployee { get; set; }
        public virtual tblEmployee tblEmployee1 { get; set; }
        public virtual tblOrg tblOrg { get; set; }
        public virtual tblStudent tblStudent { get; set; }
    }
}
