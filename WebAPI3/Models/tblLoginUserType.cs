using System;
using System.Collections.Generic;

namespace WebAPI3.Models
{
    public partial class tblLoginUserType
    {
        public long Id { get; set; }
        public long UserTypeId { get; set; }
        public string Description { get; set; }
    }
}
