using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using System;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

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

        [HttpPost]
        public ActionResult DodajWydzialDlaFirmy(string sessionId, KatWydzialy wydzialDoDodania)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _wydzialyService.DodajWydzialDlaFirmy(wydzialDoDodania, sesja.IdUzytkownika, sesja.IdUzytkownika);
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
        public ActionResult EdytujWydzialDlaFirmy(string sessionId, KatWydzialy wydzialDoEdycji)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _wydzialyService.EdytujWydzialDlaFirmy(wydzialDoEdycji, sesja.IdUzytkownika, sesja.IdUzytkownika);
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
        public ActionResult UsunWydzialZFirmy(string sessionId, KatWydzialy wydzialDoUsuniecia)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult success = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);

                    success = _wydzialyService.UsunWydzialZFirmy(wydzialDoUsuniecia, sesja.IdUzytkownika, sesja.IdUzytkownika);
                }
                result = Json(new
                {
                    success
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    success = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);

            }
            return result;
            }
        }

    }

