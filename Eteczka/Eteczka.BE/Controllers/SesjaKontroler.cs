using System.Web.Mvc;
using Eteczka.BE.Model;
using System.Collections.Generic;
using Eteczka.Model.Entities;
using System.Reflection;
using System;

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

        public ActionResult OdnowSesje(string sessionId)
        {
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                Sesja.UtworzLubAktualizujSesje(sessionId);
            }

            return Json(new
            {
                session = sessionId
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

        public ActionResult PobierzUprawnienia(string sessionId)
        {
            SessionDetails detaleSesji = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
            Uprawnienia uprawnienia = null;
            Dictionary<string, bool> nazwaDoWartosc = new Dictionary<string, bool>();

            if (detaleSesji.IsAdmin != true)
            {
                uprawnienia = detaleSesji.AktywnaFirma.Uprawnienia;
                foreach (PropertyInfo prop in uprawnienia.GetType().GetProperties())
                {
                    nazwaDoWartosc.Add(prop.Name.Replace("Rola", "").ToLower(), bool.Parse(prop.GetValue(uprawnienia, null).ToString()));
                }
            }

            return Json(new
            {
                uprawnienia = nazwaDoWartosc
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
