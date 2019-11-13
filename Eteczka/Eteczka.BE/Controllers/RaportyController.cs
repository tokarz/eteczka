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
            bool hasPermissions = false;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    hasPermissions = sesja.AktywnaFirma.Uprawnienia.RolaRaport ? true : false;

                    if (hasPermissions)
                    {
                        success = _RaportyPdfService.SkorowidzTeczkiPracownika(sesja, numeread);
                        result = Json(new
                        {
                            sucess = success
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = Json(new
                        {
                            sucess = false,
                            hasPermissions = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            if (sesja != null && numeread != null && sesja.AktywnaFirma.Firma != null)
            {
                LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.RAPORT, sesja, success, " ", " ", " ", "Files folder of employee [" + numeread.Trim() + ", company: " + sesja.AktywnaFirma.Firma.Trim() + (success ? "] PDF report generated succesfully" : "] PDF report generating attempt failure."));
            }

            return result;

        }
        public ActionResult GenerujRaportPdfSkorowidzTeczkiPelny(string sessionId, string numeread)

        {
            bool success = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            bool hasPermissions = false;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    hasPermissions = sesja.AktywnaFirma.Uprawnienia.RolaRaport ? true : false;

                    if (hasPermissions)
                    {
                        success = _RaportyPdfService.SkorowidzTeczkiPracownikaPelny(sesja, numeread);
                        result = Json(new
                        {
                            success = success
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = Json(new
                        {
                            sucess = false,
                            hasPermissions = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            if (sesja != null && numeread != null && sesja.AktywnaFirma.Firma != null)
            {
                LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.RAPORT, sesja, success, " ", " ", " ", "Files folder of employee [" + numeread.Trim() + ", company: " + sesja.AktywnaFirma.Firma.Trim() + (success ? "] PDF full report generated succesfully" : "] PDF full report generating attempt failure."));
            }

            return result;


        }

        public ActionResult GenerujRaportExcellSkorowidzPelny(string sessionId, string numeread)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            bool success = false;
            bool hasPermissions = false;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    hasPermissions = sesja.AktywnaFirma.Uprawnienia.RolaRaport ? true : false;

                    if (hasPermissions)
                    {
                        success = _RaportyExcellService.SkorowidzTeczkiExcellPelny(sesja, numeread);
                        result = Json(new
                        {
                            sucess = success
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = Json(new
                        {
                            sucess = false,
                            hasPermissions = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            if (sesja != null && numeread != null && sesja.AktywnaFirma.Firma != null)
            {
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.RAPORT, sesja, success, " ", " ", " ", "Files folder of employee [" + numeread.Trim() + ", company: " + sesja.AktywnaFirma.Firma.Trim() + (success ? "] XLSX full report generated succesfully" : "] XLSX full report generating attempt failure."));
            }

            return result;
        }
    }
}
