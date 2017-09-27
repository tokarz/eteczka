using System.Collections.Generic;
using Eteczka.Model.Entities;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.BE.Model;



namespace Eteczka.BE.Controllers
{
    public class KatDokumentyRodzajController : Controller
    {
        private KatDokumentyRodzajService _KatDokumentyRodzajService;

        public KatDokumentyRodzajController (KatDokumentyRodzajService KatDokumentyRodzajService)
        {
            this._KatDokumentyRodzajService = KatDokumentyRodzajService;
        }

        public ActionResult PobierzWszystkieRodzajeDokumentow(string sessionId)
        {
            List<KatDokumentyRodzaj> pobraneDokumenty = new List<KatDokumentyRodzaj>();
            
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pobraneDokumenty = _KatDokumentyRodzajService.PobierzRodzDok();

            }

            return Json(new
            {
                PobraneDokumenty = pobraneDokumenty
                
            }, JsonRequestBehavior.AllowGet);

        }
           

    }
}
