using WebAPI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebAPI3.Controllers
{
    //[RoutePrefix("/PersonalDetail")]
    public class PersonalDetailController : Controller
    {
        // GET: PersonalDetail
        public ActionResult Index()
        {
            return View();
        }

        //[Route("GetAllDetail")]
        webSchoolContext db = new webSchoolContext();
        public JsonResult GetAllDetail(long StudentId,long OrgId)
        {
            var Details = (from tableStudent in db.tblStudents
                           join tableStudentRegistration in db.tblStudentRegistrations on tableStudent.Id equals 
                           tableStudentRegistration.StudentId
                           join tableCourse in db.tblCourses on tableStudentRegistration.Course equals tableCourse.Id
                           join tableBatch in db.tblBatches on tableStudentRegistration.Batch equals tableBatch.Id
                           join tableOrg in db.tblOrgs on tableStudent.OrgId equals tableOrg.Id
                           where tableStudent.Id == StudentId && tableStudent.OrgId == OrgId
                           select new { FirstName = tableStudent.FirstName ,
                               MiddleName= tableStudent.MiddleName ,
                               LastName= tableStudent.LastName,tableOrg.OrgName, tableStudentRegistration.RegistrationCode,
                               Batch = tableBatch.Name, Course = tableCourse.Name}).ToList().Select(s=>new { Name = s.FirstName + " " + s.MiddleName+" "+s.LastName, s.OrgName,s.RegistrationCode,s.Batch,s.Course}).FirstOrDefault();
        
            return Json(new { Details},JsonRequestBehavior.AllowGet);
        }
        //[Route("GetEmployeeDetail")]
        public JsonResult GetEmployeeDetail(long EmployeeId,long OrgId) 
        {
            var Data = (from EmployeeTable in db.tblEmployees
                        join DesignationTable in db.tblDesignations on EmployeeTable.Designation equals DesignationTable.Id
                             into joinedDesignation from DesignationTable in joinedDesignation.DefaultIfEmpty()
                        where EmployeeTable.Id == EmployeeId && EmployeeTable.OrgId == OrgId
                        select new {
                            FirstName = EmployeeTable.FirstName,
                            MiddleName= EmployeeTable.MiddleName,
                            LastName= EmployeeTable.LastName,
                            EmployeeTable.EmpId,Designation = DesignationTable.Name}).ToList().Select(s => new { Name = s.FirstName + " " + s.MiddleName + " " + s.LastName, s.EmpId, s.Designation }).FirstOrDefault();
            return Json(new {Data},JsonRequestBehavior.AllowGet);
        }

        public FileStreamResult getEmployeeImage(long Id) 
        {
            try
            {
                var Employee = db.tblEmployees.Find(Id);
                if (Employee != null)
                {
                    return new FileStreamResult(new MemoryStream(Employee.Photo, true), "image/jpeg");
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