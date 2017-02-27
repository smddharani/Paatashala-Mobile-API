using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI3.Model_Class
{
    public class Exam
    {
        public List<examList> ExamSchedule { get; set; }
        public Exam()
        {
            ExamSchedule = new List<examList>();
        }
    }

    public class examList
    {
        public string Examtype { get; set; }
        public string SubjectName { get; set; }
        public string ExamDate { get; set; }
        public string Duration { get; set; }
        public string Starttime { get; set; }
    }
}