using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class PlikiController : Controller
    {
        private IPlikiService _PlikiService;

        public PlikiController(PlikiService plikiService)
        {
            _PlikiService = plikiService;
        }

        public ActionResult PobierzWszystkie(string sessionId)
        {
            List<Pliki> pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pliki = _PlikiService.PobierzWszystkie();
            }

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzMetadane(string sessionId, string plik)
        {
            MetaDanePliku meta = new MetaDanePliku(); ;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                meta = _PlikiService.PobierzMetadane(plik);
            }

            return Json(new
            {
                meta = meta
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzDlaUzytkownika(string sessionId, string numeread)
        {
            List<Pliki> pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pliki = _PlikiService.PobierzDlaUzytkownika(numeread);
            }

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzStanZeskanowanychPlikow(string sessionId)
        {
            StanPlikow stanPlikow = null;

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                stanPlikow = _PlikiService.PobierzStanPlikow(sessionId);
            }

            return Json(new
            {
                stan = stanPlikow
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult KomitujPlik(string sessionId, KomitPliku plik)
        {
            bool success = false;

            StanSesji sesja = Sesja.PobierzStanSesji();

            if (sesja.CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails detaleSesji = sesja.PobierzSesje(sessionId);
                success = _PlikiService.ZakomitujPlikDoBazy(plik, detaleSesji.AktywnaFirma, detaleSesji.AktywnyUser.Identyfikator);
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzGitStatusDlaFirmy(string sessionId, string firma)
        {
            List<Pliki> pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pliki = _PlikiService.PobierzPlikiDlaFirmy(firma);
            }

            return Json(new
            {
                newfiles = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzPlikiWgFiltrow (string sessionId, FiltryPlikow Filtry)
        {
            List<Pliki> Pliki = new List<Pliki>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                Pliki = _PlikiService.SzukajPlikiZFiltrow(sesja, Filtry);
            }

            return null;
        }

    }
}
