using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblBillingTemplate
    {
        public long Id { get; set; }
        public string TemplateName { get; set; }
        public string Heading { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string TermsAndConditions { get; set; }
        public string Phone { get; set; }
        public byte[] OrgPhoto { get; set; }
        public long OrgId { get; set; }
    }
}
