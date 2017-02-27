using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblBatchAdmissionArchive
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public Nullable<long> BatchId { get; set; }
        public Nullable<long> PrevBatchId { get; set; }
        public long CourseId { get; set; }
        public string Section { get; set; }
        public string RollNumber { get; set; }
        public long AdmissionStatus { get; set; }
        public long OrgId { get; set; }
        public virtual tblBatch tblBatch { get; set; }
        public virtual tblBatch tblBatch1 { get; set; }
        public virtual tblCourse tblCourse { get; set; }
        public virtual tblOrg tblOrg { get; set; }
        public virtual tblStudent tblStudent { get; set; }
    }
}
