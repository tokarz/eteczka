using System;
using System.Web.Mvc;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    [RequireHttps]
    public class SesjaController : Controller
    {

        [HttpGet]
        public ActionResult StworzSesje(string token)
        {
            string session = "@_" + new Random().Next();

            return Json(new
            {
                session = session
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UstawFirme(string name)
        {
            Sesja.FIRMA = name;

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
