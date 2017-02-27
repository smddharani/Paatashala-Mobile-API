using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class View
    {
        public long Id { get; set; }
        public string ViewName { get; set; }
        public string ViewPath { get; set; }
        public string ViewContent { get; set; }
        public long OrgId { get; set; }
    }
}
