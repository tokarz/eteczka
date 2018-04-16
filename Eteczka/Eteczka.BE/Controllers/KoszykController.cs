using Eteczka.BE.Model;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using System.Collections.Generic;
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
                if (plikiId != null && plikiId.Count > 0)
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    success = _KoszykService.UsunZKoszyka(sesja.AktywnaFirma, plikiId);
                }
            }

            return Json(new
            {
                success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WyczyscKoszyk(string sessionId)
        {
            bool success = false;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                success = _KoszykService.WyczyscKoszyk(sesja.AktywnaFirma);
            }

            return Json(new
            {
                success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DodajDoKoszyka(string sessionId, List<string> plikiId)
        {
            bool success = false;
            string blad = "";

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                if (plikiId != null)
                {
                    List<Pliki> juzDodanePliki = _KoszykService.PobierzPlikiWKoszyku(sesja.AktywnaFirma);
                    foreach (Pliki plikWKoszyku in juzDodanePliki)
                    {
                        if (plikiId.Contains("" + plikWKoszyku.Id))
                        {
                            plikiId.Remove("" + plikWKoszyku.Id);
                        }
                    }

                    success = _KoszykService.DodajPlikiDoKoszyka(sesja.AktywnaFirma, plikiId);
                }
                else
                {
                    blad = "Nie Wybrano Plikow";
                }
            }

            return Json(new
            {
                success,
                blad
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzKoszykDlaUzytkownika(string sessionId)
        {
            List<Pliki> pliki = new List<Pliki>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pliki = _KoszykService.PobierzPlikiWKoszyku(sesja.AktywnaFirma);
            }

            return Json(new
            {
                pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzIloscPlikowUzytkownika(string sessionId)
        {
            int ilosc = 0;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                ilosc = _KoszykService.PobierzIloscPlikowWKoszyku(sesja.AktywnaFirma);
            }

            return Json(new
            {
                ilosc = ilosc
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
