using System;
using System.Web.Mvc;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class SesjaController : Controller
    {
        public ActionResult StworzSesje()
        {
            string session = Sesja.UtworzSesje();

            return Json(new
            {
                session = session
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OdnowSesje(string sessionid)
        {
            Sesja.UtworzLubAktualizujSesje(sessionid);

            return Json(new
            {
                session = sessionid
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ZamknijSesje(string token)
        {
            Sesja.ZamknijSesje(token);

            return Json(new
            {
                session = token
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
