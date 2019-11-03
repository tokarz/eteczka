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

        [HttpGet]
        public ActionResult PobierzWydzialy(string sessionId)
        {
            List<KatWydzialy> PobraneWydzialy = new List<KatWydzialy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                PobraneWydzialy = _wydzialyService.PobierzWydzialyDlaFirmy(sesja.AktywnaFirma.Firma);
            }

            return Json(new
            {
                Wydzialy = PobraneWydzialy
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult PobierzWydzialyDlaFirmy(string sessionId, string firma)
        {
            List<KatWydzialy> PobraneWydzialy = new List<KatWydzialy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                PobraneWydzialy = _wydzialyService.PobierzWydzialyDlaFirmy(firma);
            }

            return Json(new
            {
                Wydzialy = PobraneWydzialy
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PobierzAktywneWydzialyDlaFirmy(string sessionId, string firma)
        {
            ActionResult result = null;
            List<KatWydzialy> WydzialyZDb = new List<KatWydzialy>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    WydzialyZDb = _wydzialyService.PobierzAktywneWydzialyDlaFirmy(firma);

                    result = Json(new
                    {
                        WydzialyZDb,
                        sucess = WydzialyZDb.Count > 0 ? true : false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpGet]
        public ActionResult PobierzNieaktywneWydzialyDlaFirmy(string sessionId, string firma)
        {
            ActionResult result = null;
            List<KatWydzialy> WydzialyZDb = new List<KatWydzialy>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    WydzialyZDb = _wydzialyService.PobierzNieaktywneWydzialyDlaFirmy(firma);

                    result = Json(new
                    {
                        WydzialyZDb,
                        sucess = WydzialyZDb.Count > 0 ? true : false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            return result;
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

        [HttpPatch]
        public ActionResult PrzywrocWydzialWFirmie(string sessionId, string firma, string wydzial)
        {
            ActionResult result = null;
            InsertResult queryResult = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    queryResult = _wydzialyService.PrzywrocWydzialWFirmieZDb(firma, wydzial, sesja.IdUzytkownika, sesja.IdUzytkownika);

                    result = Json(new
                    {
                        queryResult,

                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                result = Json(new
                {
                    queryResult = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }
    }


}

