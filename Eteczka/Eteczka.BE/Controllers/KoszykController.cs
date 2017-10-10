using Eteczka.BE.Model;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class KoszykController : Controller
    {
        private IKoszykService _KoszykService;

        public KoszykController(IKoszykService koszykService)
        {
            this._KoszykService = koszykService;
        }

        public ActionResult UsunZKoszyka(string sessionId, List<string> plikiId)
        {
            bool success = false;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                success = _KoszykService.UsunZKoszyka(sesja.AktywnaFirma, sesja.AktywnyUser, plikiId);
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WyczyscKoszyk(string sessionId)
        {
            bool success = false;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                success = _KoszykService.WyczyscKoszyk(sesja.AktywnaFirma, sesja.AktywnyUser);
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DodajDoKoszyka(string sessionId, List<string> plikiId)
        {
            bool success = false;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                success = _KoszykService.DodajPlikiDoKoszyka(sesja.AktywnaFirma, sesja.AktywnyUser, plikiId);
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzKoszykDlaUzytkownika(string sessionId)
        {
            List<Pliki> pliki = new List<Pliki>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pliki = _KoszykService.PobierzPlikiWKoszyku(sesja.AktywnaFirma, sesja.AktywnyUser);
            }

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzIloscPlikowUzytkownika(string sessionId)
        {
            int ilosc = 0;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                ilosc = _KoszykService.PobierzIloscPlikowWKoszyku(sesja.AktywnaFirma, sesja.AktywnyUser);
            }

            return Json(new
            {
                ilosc = ilosc
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
