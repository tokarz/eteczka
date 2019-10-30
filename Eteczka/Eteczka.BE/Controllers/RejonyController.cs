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
        [HttpGet]
        public ActionResult PobierzRejonyDlaWybranejFirmy(string sessionId)
        {
            List<KatRejony> PobraneRejony = new List<KatRejony>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                PobraneRejony = _rejonyService.PobierzRejonyDlaFirmy(sesja.AktywnaFirma.Firma);
            }

            return Json(new
            {
                Rejony = PobraneRejony
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PobierzRejonyDlaChwilowoWybranejFirmy(string sessionId, string firma)
        {
            List<KatRejony> PobraneRejony = new List<KatRejony>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                PobraneRejony = _rejonyService.PobierzRejonyDlaFirmy(firma);
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

        [HttpPut]
        public ActionResult EdytujRejonDlaFirmy(string sessionId, string rejonWBazie, KatRejony rejonDoEdycji)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _rejonyService.EdytujRejonDlaFirmy(rejonDoEdycji,  sesja.IdUzytkownika, sesja.IdUzytkownika);
                    
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
        [HttpPut]
        public ActionResult UsunRejon(string sessionId, string firma, string rejon)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult wynikInserta = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    wynikInserta = _rejonyService.UsunRejon(firma, rejon, sesja.IdUzytkownika, sesja.IdUzytkownika);
                }
                result = Json(new
                {
                    wynikInserta
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    wynikInserta = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        public ActionResult WyszukajRejony (string sessionId, string firma, string search)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            List<KatRejony> WyszukaneRejony = new List<KatRejony>();

            try
            {
                if ( Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    WyszukaneRejony = _rejonyService.WyszukajRejon(firma, search);
                }
                result = Json(new
                {
                    sucess = WyszukaneRejony.Count > 0 ? true : false,
                    WyszukaneRejony
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
