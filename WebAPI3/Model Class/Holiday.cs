using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI3.Model_Class
{
    public class Holiday
    {
        public List<days> HolidaysList { get; set; }
        public Holiday()
        {
            HolidaysList = new List<days>();
        }
    }
    public class days
    {
        public string Name { get; set; }
        public string Date { get; set; }
    }
}