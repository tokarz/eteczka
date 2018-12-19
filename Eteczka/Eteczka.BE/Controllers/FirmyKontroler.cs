using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using System;

namespace Eteczka.BE.Controllers
{
    public class FirmyController : Controller
    {
        private IFirmyService _FirmyService;

        public FirmyController(IFirmyService firmyService)
        {
            this._FirmyService = firmyService;
        }

        public ActionResult PobierzWszystkieFirmy(string sessionId)
        {
            List<KatFirmy> pobraneFirmy = new List<KatFirmy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pobraneFirmy = _FirmyService.PobierzWszystkie();
            }

            return Json(new
            {
                Firmy = pobraneFirmy
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UstawAktywnaFirme(string sessionID, string company)
        {
            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionID))
            {

                success = Sesja.PobierzStanSesji().UstawAktywnaFirme(sessionID, company.Trim());
                
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzAktywnaFirme(string sessionId)
        {
            string firma = "";
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma.Firma.Trim();
            }

            return Json(new
            {
                firma = firma
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DodajFirme(string sessionId, KatFirmy firmaDoDodania)
        {

            ActionResult result = null;
            InsertResult sucess = new InsertResult();
            SessionDetails sesja = null;
            try
            {

                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _FirmyService.DodajFirme(firmaDoDodania, sesja.IdUzytkownika, sesja.IdUzytkownika);

                    result = Json(new
                    {
                        sucess
                    }, JsonRequestBehavior.AllowGet);
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

            return result;
        }
    }
}

