using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI3.Models;

namespace WebAPI3.Controllers
{
    public class ParentNotificatonController : Controller
    {

        webSchoolContext db = new webSchoolContext();
        //
        // GET: /ParentNotificaton/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNotificationAll(long StudentId,long OrgId,long BatchId,long CourseId)
        {
            var Now = DateTime.Now;
            var today = Now.Date;
            var Notifications = (from tableStudentFeePaidHeader in db.tblStudentFeePaidHeaders
                                 where tableStudentFeePaidHeader.OrgId == OrgId && tableStudentFeePaidHeader.StudentId == StudentId &&
                                 tableStudentFeePaidHeader.Batch == BatchId && tableStudentFeePaidHeader.Course == CourseId &&
                                 tableStudentFeePaidHeader.DueDate == today
                                 select new { tableStudentFeePaidHeader.StudentId, tableStudentFeePaidHeader.DueDate, tableStudentFeePaidHeader.Narration }).ToList().Select(s => new { s.StudentId, s.Narration, Date = s.DueDate.ToString() });
            return Json(new { Notifications},JsonRequestBehavior.AllowGet);
        }

    }
}
