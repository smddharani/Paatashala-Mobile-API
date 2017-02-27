using Newtonsoft.Json;
using WebAPI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Mvc;
using System.IO;

namespace WebAPI3.Controllers
{
    public class StudentController : Controller
    {
        webSchoolContext db = new webSchoolContext();
        public JsonResult GetStudents(long batchId, long courseId, long OrgId)
        {
            try
            {
                var students = (from ba in db.tblBatchAdmissions
                              join b in db.tblBatches on ba.BatchId equals b.Id
                              join c in db.tblCourses on ba.CourseId equals c.Id
                              join s in db.tblStudents on ba.StudentId equals s.Id
                              where ba.OrgId == OrgId && ba.CourseId == courseId && ba.BatchId == batchId
                              select new { s.Id, Name = s.FirstName + " " + s.MiddleName + " " + s.LastName, s.FatherName, s.Sex, s.DateOfBirth,ba.BatchId,ba.CourseId}).ToList();
                return Json(students, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public FileStreamResult StudentImage(long Id)
        {
            try
            {
                var student = db.tblStudents.Find(Id);
                if (student != null)
                {
                    return new FileStreamResult(new MemoryStream(student.Photo, true), "image/jpeg");
                }
                return new FileStreamResult(new FileStream(@"~\Resources\Image\student.png", FileMode.Open), "image/jpeg");
            }
            catch
            {
                return null;

            }

        }
    }
}
