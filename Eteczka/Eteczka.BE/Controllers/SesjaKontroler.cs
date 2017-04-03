using System;
using System.Web.Mvc;

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

    }
}
