using System.Web.Mvc;
using Eteczka.BE.Model;
using System.Collections.Generic;

namespace Eteczka.BE.Controllers
{
    public class SesjaController : Controller
    {
        public ActionResult StworzSesje()
        {
            SessionDetails sesja = Sesja.UtworzSesje();

            return Json(new
            {
                session = sesja
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

        public ActionResult ZamknijSesje(string sessionID, string toKill)
        {
            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionID))
            {
                Sesja.ZamknijSesje(toKill);
                success = true;
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzOtwarteSesje(string sessionId)
        {
            List<SessionDetails> sesje = new List<SessionDetails>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                sesje = Sesja.PobierzStanSesji().PobierzOtwarteSesje();
            }

            return Json(new
            {
                sesje = sesje
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
