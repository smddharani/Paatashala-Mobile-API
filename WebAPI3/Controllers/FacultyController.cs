﻿using WebAPI3.Model_Class;
using WebAPI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI3.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        public ActionResult Index()
        {
            return View();
        }
        webSchoolContext db = new webSchoolContext();
        Faculty Facultyobj = new Faculty();
        public JsonResult GetFaculty(long StudentId)
        {
            try
            {
                var FacultyData = (from tableSubject in db.tblSubjects
                                    join tableCourseSubjects in db.tblCourse_Subject on tableSubject.Id equals tableCourseSubjects.SubjectId
                                    join tableBatchAdmission in db.tblBatchAdmissions on tableCourseSubjects.CourseId equals tableBatchAdmission.CourseId
                                    join tableTimeTable in db.tblTimeTableDetails on tableSubject.Id equals tableTimeTable.SubjectId
                                    join timetable in db.tblTimeTables on tableTimeTable.TimeTableId equals timetable.Id
                                    join tableEmployee in db.tblEmployees on tableTimeTable.FacultyId equals tableEmployee.Id
                                    where tableBatchAdmission.StudentId == StudentId && timetable.IsActive == true
                                    select new { tableSubject.Name, FirstName = tableEmployee.FirstName, MiddleName=tableEmployee.MiddleName, LastName=tableEmployee.LastName }).ToList().Select(s=>new {s.Name, EmployeeName=s.FirstName+" "+s.MiddleName+" "+s.LastName}).Distinct();

                Facultyobj.FacultyList = new List<Faculties>();
                foreach(var item in FacultyData)
                {
                    var facultytemp = new Faculties();
                    Facultyobj.FacultyList.Add(facultytemp);
                    facultytemp.EmployeeName = item.EmployeeName;
                    facultytemp.SubjectName = item.Name;
                }

                return Json(Facultyobj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}