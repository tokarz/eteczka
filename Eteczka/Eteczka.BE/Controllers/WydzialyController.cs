using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;

namespace Eteczka.BE.Controllers
{
    public class WydzialyController : Controller
    {
        private IWydzialyService _wydzialyService;

        public WydzialyController(IWydzialyService wydzialyService)
        {
            this._wydzialyService = wydzialyService;
        }

        public ActionResult PobierzWydzialy(string firma)
        {
            List<KatWydzialy> PobraneWydzialy = _wydzialyService.PobierzWydzialyDlaFirmy(firma);

            return Json(new
            {
                Wydzialy = PobraneWydzialy
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
