using WebAPI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebAPI3.Controllers
{
    public class GeoLocationController : Controller
    {
        // GET: GeoLocation
        public ActionResult Index()
        {
            return View();
        }
        webSchoolContext db = new webSchoolContext();
        private DateTime ConvertToDateTime(string input)
        {
            DateTime dateTime;
            if (DateTime.TryParse(input, out dateTime))
            {
                return dateTime;
            }
            return DateTime.Now;
        }

        public JsonResult GetRouteCode(long OrgId)
        {
            var Routes = db.tblRoutes.Where(x => x.OrgId == OrgId).Select(x => new { x.Id, x.RouteCode }).ToList();
            return Json(new { Routes }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateRouteLocation(long Routecode, long OrgId, string Lattitude, string Longitude)
        {
            try
            {
                System.IO.File.AppendAllText(Server.MapPath("log.txt"), string.Format("{0} - Route:{1} Lat:{2} Long:{3}{4}", DateTime.Now, Routecode, Lattitude, Longitude, Environment.NewLine));
                var datee = DateTime.Now;
                db.tblGPSDatas.Add(new tblGPSData()
                {
                    RouteCode = Routecode,
                    OrgId = OrgId,
                    Latitude = float.Parse(Lattitude),
                    Longitude = float.Parse(Longitude),
                    Time = new DateTime(datee.Year, datee.Month, datee.Day, datee.Hour, datee.Minute, datee.Second)
                });
                db.SaveChanges();
                return Json("Saved Successfully", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetStrLocation(string Routecode, string OrgId, string Lattitude, string Longitude, string nowDateTime)
        {
            try
            {
                var datee = ConvertToDateTime(nowDateTime);
                db.tblGPSDatas.Add(new tblGPSData()
                {
                    RouteCode = Convert.ToInt64(Routecode),
                    OrgId = Convert.ToInt64(OrgId),
                    Latitude = float.Parse(Lattitude),
                    Longitude = float.Parse(Longitude),
                    Time = new DateTime(datee.Year, datee.Month, datee.Day, datee.Hour, datee.Minute, datee.Second)
                });
                db.SaveChanges();
                return Json("Saved Successfully", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ShowLocation(long Routecode, long OrgId)
        {
            var ExactLocation = db.tblGPSDatas.Where(x => x.OrgId == OrgId && x.RouteCode == Routecode).OrderByDescending(o => o.Time).Take(1).Select(x => new { x.Latitude, x.Longitude }).FirstOrDefault();
            return Json(ExactLocation, JsonRequestBehavior.AllowGet);
        }
    }
}