using Eteczka.BE.Model;
using Eteczka.BE.Services;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class RaportyController : Controller
    {
        private IRaportyPdfService _RaportyPdfService;
        public RaportyController(IRaportyPdfService raportyPdfService)
        {
            this._RaportyPdfService = raportyPdfService;
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
    }
}
