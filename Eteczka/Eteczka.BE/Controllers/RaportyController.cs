using Eteczka.BE.Model;
using Eteczka.BE.Services;
using System;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class RaportyController : Controller
    {
        private IRaportyPdfService _RaportyPdfService;
        private IRaportyExcellService _RaportyExcellService;
        public RaportyController(IRaportyPdfService raportyPdfService, IRaportyExcellService raportyExcellService)
        {
            this._RaportyPdfService = raportyPdfService;
            this._RaportyExcellService = raportyExcellService;
        }
        public ActionResult GenerujRaportPdfSkorowidzTeczki(string sessionId, string numeread)

        {
            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                success = _RaportyPdfService.SkorowidzTeczkiPracownika(sesja, numeread);
            }
            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GenerujRaportPdfSkorowidzTeczkiPelny(string sessionId, string numeread)

        {
            bool success = false;
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    success = _RaportyPdfService.SkorowidzTeczkiPracownikaPelny(sesja, numeread);
                }
                result = Json(new
                {
                    success = success
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
            
            
        }

        public ActionResult GenerujRaportExcellSkorowidzPelny(string sessionId, string numeread)

        {
            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                success = _RaportyExcellService.SkorowidzTeczkiExcellPelny(sesja, numeread);
            }
            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
