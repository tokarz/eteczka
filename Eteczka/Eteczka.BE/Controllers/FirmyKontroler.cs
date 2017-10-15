using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using Eteczka.BE.Model;


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
                firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma;
            }

            return Json(new
            {
                firma = firma
            }, JsonRequestBehavior.AllowGet);
        }
    }
}

