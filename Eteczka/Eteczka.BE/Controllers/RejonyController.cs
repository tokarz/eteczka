using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using System;
using Eteczka.BE.Model;


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
        public ActionResult PobierzRejonyDlaWybranejFirmy(string sessionId)
        {

            List<KatRejony> PobraneRejony = new List<KatRejony>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                PobraneRejony = _rejonyService.PobierzRejonyDlaFirmy(sesja);
            }
            
            return Json(new
            {
                Rejony = PobraneRejony
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
