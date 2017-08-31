using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;
using Eteczka.DB.Entities;
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

        public ActionResult PobierzWszystkie()
        {
            List<Pliki> pliki = _PlikiService.PobierzWszystkie();

            return Json(new
            {
                pliki = pliki
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzMetadane(string plik)
        {
            MetaDanePliku meta = _PlikiService.PobierzMetadane(plik);

            return Json(new
            {
                meta = meta
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzStanZeskanowanychPlikow(string sessionId)
        {
            StanPlikow stanPlikow = null;
            if(Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                stanPlikow = _PlikiService.PobierzStanPlikow(sessionId);
            }

            return Json(new
            {
                stan = stanPlikow
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
