using WebAPI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI3.Model_Class;

namespace WebAPI3.Controllers
{
    //[RoutePrefix("/FeeDetail")]
    public class FeeDetailController : Controller
    {
        // GET: FeeDetail
        public ActionResult Index()
        {
            return View();
        }
        private string ConvertToJavascriptDate(DateTime dateTime)
        {
            return dateTime.ToString("ddd MMM dd yyyy HH:mm:ss") + " GMT+0530";
        }
        //[Route("GetByStudent")]
        public JsonResult GetByStudent(long StudentId)
        {
            webSchoolContext db = new webSchoolContext();
            var feeDetails = (from tablefee in db.tblStudentFeePaidHeaders
                              join tablePaymentMode in db.tblFeePaymentModes on tablefee.PaymentMode equals tablePaymentMode.Id into joinMode from tablePaymentMode in joinMode.DefaultIfEmpty()
                              where tablefee.StudentId == StudentId
                              select new { DatePaid = tablefee.DatePaid, tablefee.GrandTotal, tablefee.PaidForPeriod,PaymentMode=tablePaymentMode.Name}).ToList().Select(m => new { DatePaid=m.DatePaid.ToString(),m.GrandTotal,m.PaidForPeriod,m.PaymentMode}).OrderByDescending(o=> o.DatePaid);
            return Json(new {feeDetails}, JsonRequestBehavior.AllowGet);
        }
    }
}