using WebAPI3.Model_Class;
using WebAPI3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebAPI3.Controllers
{
    public class AttandanceController : Controller
    {
        webSchoolContext DbContext = new webSchoolContext();
        // GET: Attandance
        public ActionResult Index()
        {
            return View();
        }

        private DateTime ConvertToDateTime(string input)
        {
            DateTime dateTime;
            if (DateTime.TryParse(input, out dateTime))
            {
                return dateTime;
            }
            return DateTime.Now;
        }

        public JsonResult getStudentsList(long OrgId)
        {
            var AdmStudents = (from tableStudent in DbContext.tblStudents
                               join tableBatchAdmission in DbContext.tblBatchAdmissions on tableStudent.Id equals tableBatchAdmission.StudentId
                               where tableBatchAdmission.OrgId == OrgId && tableStudent.IsLead == false
                               select new { FirstName = tableStudent.FirstName,MiddleName=tableStudent.MiddleName,LastName=tableStudent.LastName, tableStudent.Id,tableStudent.StudentId}).ToList().Select(s=>new { Name = s.FirstName+" "+ s.MiddleName+" "+ s.LastName, s.Id,s.StudentId });
            return Json(new { AdmStudents }, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult SaveAttendance(long OrgId,string StudentId,string scanDateTime,bool IsCheckIn)
        { 
            try
            {
                var datee = ConvertToDateTime(scanDateTime);
                string[] arrlong=null;
                if (StudentId.Contains(','))
                {
                 arrlong = StudentId.Split(',');
                }
                else
                {
                    arrlong = new string[] { StudentId};
                }
                   
                foreach (var temp in arrlong)
                {
                    DbContext.tblAttendanceHourlies.Add(new tblAttendanceHourly()
                    {
                        StudentId = Convert.ToInt64(temp),
                        IsPickUp = IsCheckIn,
                        IsAttendance = true,
                        Time = new TimeSpan(datee.Hour, datee.Minute, datee.Second),
                        Date = new DateTime(datee.Year, datee.Month, datee.Day),
                        OrgId = OrgId
                    });
                    DbContext.SaveChanges();
                }
                    return Json(new{ status = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, message = e.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveTransport (long OrgId,string StudentId,string scanDateTime,bool IsPickUp)
        {
            string Status = "";
            try
            {
                var datee = ConvertToDateTime(scanDateTime);
                string[] arrlong = null;
                if (StudentId.Contains(','))
                {
                    arrlong = StudentId.Split(','); 
                }
                else
                {
                    arrlong = new string[] { StudentId };
                }

                foreach (var temp in arrlong)
                {
                    DbContext.tblAttendanceHourlies.Add(new tblAttendanceHourly()
                    {
                        StudentId = Convert.ToInt64(temp),
                        IsPickUp = IsPickUp,
                        Time = new TimeSpan(datee.Hour, datee.Minute, datee.Second),
                        Date = new DateTime(datee.Year, datee.Month, datee.Day),
                        IsAttendance = false,
                        OrgId = OrgId
                    });
                    DbContext.SaveChanges();

                    var SId = Convert.ToInt64(temp);
                    var Student = DbContext.tblStudents.Where(x => x.Id == SId).Select(y => y).FirstOrDefault();
                    string Number = "+91" + Student.ContactNo;
                    string Username = "sftg-softserve";
                    string Password = "soft123";
                    string Source = "SSGBLR";
                    if (IsPickUp == true) { Status = "Pickedup"; }
                    if (IsPickUp == false) { Status = "Dropped"; }

                    string Content = "Its Here By To Notify Your Kid Master. " + Student.FirstName + " " + Student.MiddleName + " " + Student.LastName + " " + "Has " + Status;

                    string strUrl = "http://103.16.101.52:8080/sendsms/bulksms?username=" + Username + "&password=" + Password + "&type=0&dlr=1&destination=" + Number + "&source=" + Source + "&message=" + Content;
                    HttpWebRequest _createRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                    HttpWebResponse response = (HttpWebResponse)_createRequest.GetResponse();
                    Stream s = (Stream)response.GetResponseStream();
                    StreamReader readStream = new StreamReader(s);
                    string dataString = readStream.ReadToEnd();
                    response.Close();
                    s.Close();
                    readStream.Close();
                }
                return Json(new {status = true },JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { status = false, message = e.ToString()},JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}