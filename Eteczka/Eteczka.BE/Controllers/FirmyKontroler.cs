using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;


namespace Eteczka.BE.Controllers
{
    public class FirmyController : Controller
    {
        private IFirmyService _firmyService;

        public FirmyController(IFirmyService firmyService)
        {
            this._firmyService = firmyService;
        }

        public ActionResult PobierzWszystkieFirmy()
        {
            List<KatFirmy> pobraneFirmy = _firmyService.PobierzWszystkie();

            return Json(new
            {
                Firmy = pobraneFirmy
            }, JsonRequestBehavior.AllowGet);
        }




    }
}

