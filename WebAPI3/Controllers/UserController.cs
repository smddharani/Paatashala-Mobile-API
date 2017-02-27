using WebAPI3.HelperObject;
using WebAPI3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebAPI3.Controllers
{
    //[RoutePrefix("/User")]
    public class UserController : Controller
    {
        webSchoolContext db = new webSchoolContext();
        //[Route("Login")]
        public JsonResult Login(string Username, string Password)
        {
            string message = string.Empty;
            try
            {
                
                var data = (from tableLogin in db.tblAppLogins
                            where tableLogin.Email == Username && tableLogin.IsRegistered == true
                            select new { Password = tableLogin.Password,Username = tableLogin.Email ,tableLogin.Id }).SingleOrDefault();
                if (data != null)
                {
                    var Pwd = SMSDataformatter.DecryptText(data.Password);
                    if (SMSDataformatter.DecryptText(data.Password) == Password)
                    {
                        var HasStudents = (from StudentTable in db.tblStudents
                                           join StudentRegistration in db.tblStudentRegistrations on StudentTable.Id equals StudentRegistration.StudentId
                                           where StudentTable.FatherEmail == Username || StudentTable.MotherEmail == Username
                                           select new {StudentTable.Id, StudentTable.FirstName,StudentTable.MiddleName,
                                           StudentTable.LastName, StudentRegistration.RegistrationCode, StudentRegistration.Course,
                                               StudentRegistration.Batch, StudentTable.OrgId, StudentTable.FatherName, StudentTable.Sex}).ToList().Select(s => new { Name = s.FirstName + " " + s.MiddleName + " " + s.LastName, s.Id,s.Batch,s.Course,s.FatherName,s.RegistrationCode,s.OrgId,s.Sex });
                        return Json(new {Status = true, User = new { Username = data.Username, UserId = data.Id,}, HasStudents },JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        message = string.Format("Password wrong!", Username);
                        return Json(new { Status = false, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    message = string.Format("{0} not exists!", Username);
                    return Json(new { Status = false, Message = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { Status = false, Message = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //[Route("EmployeeLogin")]
        public JsonResult EmployeeLogin(string Username,string Password,string OrgName)
        {
            string message = string.Empty;
            try
            {
                var OrgDetails = GetOrgDetails(OrgName.Trim().ToLower());
                if (OrgDetails != null)
                {
                    long loggedUserOrgid = Convert.ToInt64(OrgDetails.GetType().GetProperty("OrgId").GetValue(OrgDetails, null));
                    var loggedUser = db.tblLogins.Where(x => x.UserName.ToLower() == Username.ToLower() &&
                        x.OrgId == loggedUserOrgid && x.IsActive == true).Select(x => x).SingleOrDefault();
                    if (loggedUser != null)
                    {
                        if (SMSDataformatter.DecryptText(loggedUser.Password) == Password)
                        {
                            return Json(new {Status=true,User = new {Username = loggedUser.UserName,OrgId=loggedUser.OrgId,UserId= loggedUser.UserId}},JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            message = string.Format("Password wrong!", Username);
                            return Json(new { Status = false, Message = message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        message = string.Format("{0} not exists!", Username);
                        return Json(new { Status = false, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    message = string.Format("Organiztion Name not exists!");
                    return Json(new { Status = false, Message = message }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                return Json(new { message = e.ToString(),status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        private object GetOrgDetails(string orgName)
        {
            webSchoolContext db = new webSchoolContext();
            return db.tblOrgs.Where(x => x.OrgName.ToLower() == orgName).Select(x => new { OrgId = x.Id, OrgName = x.OrgName }).SingleOrDefault();
        }
        public JsonResult UpdateToken(long UserId,string SenderId)
        {
            var User = db.tblAppLogins.Where(x => x.Id == UserId).Select(y => y).SingleOrDefault();
            User.SenderId = SenderId;
            db.Entry(User).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new {status = true},JsonRequestBehavior.AllowGet);
        }

    }
}
