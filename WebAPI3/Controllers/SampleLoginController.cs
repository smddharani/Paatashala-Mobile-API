using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI3.Models;

namespace WebAPI3.Controllers
{
    public class SampleLoginController : Controller
    {
        //
        // GET: /SampleLogin/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult VerifyLogin(string Username,string Password)
        {
            webSchoolContext db = new webSchoolContext();
            db.tblAppLogins.Add(new tblAppLogin()
            {
                Email = Username,
                Password = Password

            });
            db.SaveChanges();
            return Json("Saved Successfully",JsonRequestBehavior.AllowGet);
        }

    }
}
