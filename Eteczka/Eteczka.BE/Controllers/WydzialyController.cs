using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using System;
using Eteczka.BE.Model;


namespace Eteczka.BE.Controllers
{
    public class WydzialyController : Controller
    {
        private IWydzialyService _wydzialyService;

        public WydzialyController(IWydzialyService wydzialyService)
        {
            this._wydzialyService = wydzialyService;
        }

        public ActionResult PobierzWydzialy(string sessionId)
        {
            List<KatWydzialy> PobraneWydzialy = new List<KatWydzialy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                PobraneWydzialy = _wydzialyService.PobierzWydzialyDlaFirmy(sesja);
            }
         

            return Json(new
            {
                Wydzialy = PobraneWydzialy
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
