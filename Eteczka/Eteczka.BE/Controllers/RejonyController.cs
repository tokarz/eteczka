using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using System;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Controllers
{
    public class RejonyController : Controller
    {
        private IRejonyService _rejonyService;

        public RejonyController(IRejonyService rejonyService)
        {
            this._rejonyService = rejonyService;
        }

        public ActionResult PobierzWszystkieRejony(string sessionId)
        {
            List<KatRejony> pobraneRejony = new List<KatRejony>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pobraneRejony = _rejonyService.PobierzRejony();
            }

            return Json(new
            {
                Rejony = pobraneRejony
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

        [HttpPost]
        public ActionResult DodajRejonDlaFirmy(string sessionId, KatRejony rejonDoDodania)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _rejonyService.DodajRejonDlaFirmy(rejonDoDodania, sesja.IdUzytkownika, sesja.IdUzytkownika);
                }

                result = Json(new
                {
                    sucess

                }, JsonRequestBehavior.AllowGet);
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
