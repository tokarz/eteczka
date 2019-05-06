﻿using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using System;

namespace Eteczka.BE.Controllers
{
    public class FirmyController : Controller
    {
        private IFirmyService _FirmyService;

        public FirmyController(IFirmyService firmyService)
        {
            this._FirmyService = firmyService;
        }

        public ActionResult PobierzWszystkieFirmy(string sessionId)
        {
            List<KatFirmy> pobraneFirmy = new List<KatFirmy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                pobraneFirmy = _FirmyService.PobierzWszystkie();
            }

            return Json(new
            {
                Firmy = pobraneFirmy
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PobierzWszystkieAktywneFirmy(string sessionId)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            List<KatFirmy> PobraneFirmy = new List<KatFirmy>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    PobraneFirmy = _FirmyService.PobierzWszystkieAktywneFirmy();
                }
                result = Json(new
                {
                    PobraneFirmy,
                    sucess = PobraneFirmy.Count > 0 ? true : false

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

        [HttpGet]
        public ActionResult PobierzWszystkieNieaktywneFirmy(string sessionId)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            List<KatFirmy> PobraneFirmy = new List<KatFirmy>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    PobraneFirmy = _FirmyService.PobierzWszystkieNieaktywneFirmy();
                }
                result = Json(new
                {
                    PobraneFirmy,
                    sucess = PobraneFirmy.Count > 0 ? true : false

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

        public ActionResult UstawAktywnaFirme(string sessionID, string company)
        {
            bool success = false;
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionID))
            {

                success = Sesja.PobierzStanSesji().UstawAktywnaFirme(sessionID, company.Trim());
                
            }

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzAktywnaFirme(string sessionId)
        {
            string firma = "";
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma.Firma.Trim();
            }

            return Json(new
            {
                firma = firma
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult WyszukajFirmePoNipie(string sessionId, string nipWBazie)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            KatFirmy znalezionaFirma = null;
            try
            {
                if ( Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {

                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    znalezionaFirma = _FirmyService.WyszukajFirmePoNipie(nipWBazie);
                }
                result = Json(new
                {
                    sucess = znalezionaFirma != null ? true : false,
                    znalezionaFirma

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

        [HttpPost]
        public ActionResult DodajFirme(string sessionId, KatFirmy firmaDoDodania)
        {

            ActionResult result = null;
            InsertResult sucess = new InsertResult();
            SessionDetails sesja = null;
            try
            {

                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _FirmyService.DodajFirme(firmaDoDodania, sesja.IdUzytkownika, sesja.IdUzytkownika);

                    result = Json(new
                    {
                        sucess
                    }, JsonRequestBehavior.AllowGet);
                }
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
        public ActionResult EdytujFirme(string sessionId, string nipWBazie, KatFirmy firmaDoEdycji)
        {
            ActionResult result = null;
            InsertResult sucess = new InsertResult();
            SessionDetails sesja = null;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _FirmyService.EdytujFirme(firmaDoEdycji, nipWBazie, sesja.IdUzytkownika, sesja.IdUzytkownika);
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
        public ActionResult UsunFirme(string sessionId, string nip)
        {
            ActionResult result = null;
            InsertResult sucess = new InsertResult();
            SessionDetails sesja = null;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _FirmyService.UsunFirme(nip, sesja.IdUzytkownika, sesja.IdUzytkownika);
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
        [HttpGet]
        public ActionResult WyszukajFirmePoNipieFirmieLubNazwie(string sessionId, string search)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            List<KatFirmy> FirmyZDB = new List<KatFirmy>();
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    FirmyZDB = _FirmyService.WyszukajFirmePoNipieFirmieLubNazwie(search);

                }
                result = Json(new
                {
                    FirmyZDB,
                    sucess = true

                }, JsonRequestBehavior.AllowGet);
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

    }
}

