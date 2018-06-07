using Eteczka.BE.Model;
using Eteczka.BE.Services;
using Eteczka.Utils.Logger;
using Eteczka.Utils.Common.DTO;
using System;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class RaportyController : Controller
    {
        IEadLogger LOGGER = LoggerFactory.GetLogger();
        private IRaportyPdfService _RaportyPdfService;
        private IRaportyExcellService _RaportyExcellService;
        public RaportyController(IRaportyPdfService raportyPdfService, IRaportyExcellService raportyExcellService)
        {
            this._RaportyPdfService = raportyPdfService;
            this._RaportyExcellService = raportyExcellService;
        }
        public ActionResult GenerujRaportPdfSkorowidzTeczki(string sessionId, string numeread)
        {
            ActionResult result = null;
            bool success = false;
            SessionDetails sesja = null;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    success = _RaportyPdfService.SkorowidzTeczkiPracownika(sesja, numeread);
                }

                result = Json(new
                {
                    sucess = success
                }, JsonRequestBehavior.AllowGet);

                LOGGER.LOG(PoziomLogowania.INFO, Akcja.RAPORT, "Generowanie raport PDF: Skorowidz teczki. Pracownik: " + numeread.Trim() + ", firma: " + sesja.AktywnaFirma.Firma.Trim(), success, sesja);
            }
            catch (Exception)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;

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
            ActionResult result = null;
            SessionDetails sesja = null;
            bool success = false;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    success = _RaportyExcellService.SkorowidzTeczkiExcellPelny(sesja, numeread);
                }
                result = Json(new
                {
                    sucess = success
                }, JsonRequestBehavior.AllowGet);

                LOGGER.LOG(PoziomLogowania.INFO, Akcja.RAPORT, "Generowanie raportu XLSX: Pełny skorowidz teczki pracownika " + numeread.Trim() + ", firma: " + sesja.AktywnaFirma.Firma.Trim(), success, sesja);
            }
            catch (Exception)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            
            return result;
        }
    }
}
