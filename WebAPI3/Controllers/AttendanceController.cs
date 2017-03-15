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
                DuplicateAttendance DuplicateAttendanceObj = new DuplicateAttendance();
                var datee = ConvertToDateTime(scanDateTime);
                var NowDate = new DateTime(datee.Year, datee.Month, datee.Day);
                string[] arrlong=null;
                if (StudentId.Contains(','))
                {
                 arrlong = StudentId.Split(',');
                }
                else
                {
                    arrlong = new string[] { StudentId};
                }
                DuplicateAttendanceObj.DuplicatesList = new List<Dupicates>();
              
                if (IsCheckIn == true)
                {
                    foreach (var temp in arrlong)
                    {
                        long StudId = Convert.ToInt64(temp);
                        var DuplicateCheckIn = DbContext.tblAttendanceHourlies.Where(x => x.OrgId == OrgId && x.StudentId == StudId && x.Date == NowDate && x.IsAttendance == true).OrderByDescending(x => x.Time).Take(1).Select(y => y).FirstOrDefault();
                        if(DuplicateCheckIn == null || DuplicateCheckIn.IsPickUp == false)
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
                        else
                        {
                            var StudnetDuplicate = new Dupicates();
                            DuplicateAttendanceObj.DuplicatesList.Add(StudnetDuplicate);
                            StudnetDuplicate.StudentId = Convert.ToInt64(temp);
                            DuplicateAttendanceObj.ReasonDuplicate = "Already Checked In";
                        }
                    }
                    return Json(new { status = true, DuplicateAttendanceObj }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    foreach (var temp in arrlong)
                    {
                        long StudId = Convert.ToInt64(temp);
                        var Dup = DbContext.tblAttendanceHourlies.Where(x => x.OrgId == OrgId && x.StudentId == StudId && x.Date == NowDate && x.IsAttendance == true && x.IsPickUp == true).OrderByDescending(x => x.Time).Take(1).Select(y => y).FirstOrDefault();
                        if(Dup != null)
                        {
                            var d = new TimeSpan(Dup.Time.Hours, Dup.Time.Minutes, Dup.Time.Seconds);
                            var e = new TimeSpan(datee.Hour, datee.Minute, datee.Second);
                            TimeSpan f = e.Subtract(d);
                            if(f.Hours >= 2)
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
                            else
                            {
                                var StudnetDuplicate = new Dupicates();
                                DuplicateAttendanceObj.DuplicatesList.Add(StudnetDuplicate);
                                StudnetDuplicate.StudentId = Convert.ToInt64(temp);
                                DuplicateAttendanceObj.ReasonDuplicate = "Below 2 Hrs Checkin";
                            }
                        }
                        else
                        {
                            var StudnetDuplicate = new Dupicates();
                            DuplicateAttendanceObj.DuplicatesList.Add(StudnetDuplicate);
                            StudnetDuplicate.StudentId = Convert.ToInt64(temp);
                            DuplicateAttendanceObj.ReasonDuplicate = "Not Checkin can't CheckOut";
                        }
                    }
                    return Json(new { status = true, DuplicateAttendanceObj }, JsonRequestBehavior.AllowGet);
                }
                   
            }
            catch (Exception e)
            {
                return Json(new { status = false, message = e.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveTransport (long OrgId,string StudentId,string scanDateTime,bool IsPickUp,string Position)
        {
            string Status = "";
            string Place = "";
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
                    string Number = "+91" + Student.FatherContactNo;
                    string Username = "sftg-softserve";
                    string Password = "soft123";
                    string Source = "SSGBLR";
                    if(Position == "School")
                    {
                        if(IsPickUp == true)
                        {
                            Status = "This message is to notify your kid "+ Student.FirstName + " " + Student.MiddleName + " " + Student.LastName +"has boarded the vehicle for school.";
                        }
                        else if(IsPickUp == false)
                        {
                            Status = "This message is to notify your kid "+ Student.FirstName + " " + Student.MiddleName + " " + Student.LastName + "has reached the school.";
                        }
                    }
                    else if (Position == "Home")
                    {
                        if (IsPickUp == true)
                        {
                            Status = "This message is to notify your kid "+ Student.FirstName + " " + Student.MiddleName + " " + Student.LastName + "has boarded the vehicle for home.";
                        }
                        else if (IsPickUp == false)
                        {
                            Status = "This message is to notify your kid "+ Student.FirstName + " " + Student.MiddleName + " " + Student.LastName + "has reached the home.";
                        }
                    }
                    string Content = Status;
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