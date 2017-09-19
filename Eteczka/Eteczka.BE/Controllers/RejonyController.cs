using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;

namespace Eteczka.BE.Controllers
{
    public class RejonyController : Controller
    {
        private IRejonyService _rejonyService;

        public RejonyController(IRejonyService rejonyService)
        {
            this._rejonyService = rejonyService;
        }

        public ActionResult PobierzWszystkieRejony()
        {
            List<KatRejony> PobraneRejony = _rejonyService.PobierzRejony();

            return Json(new
            {
                Rejony = PobraneRejony
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PobierzRejonyDlaWybranejFirmy(string firma)
        {
            List<KatRejony> PobraneRejony = _rejonyService.PobierzRejonyDlaFirmy(firma);
            return Json(new
            {
                Rejony = PobraneRejony
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
