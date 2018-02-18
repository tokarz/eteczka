using System.Linq;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using System.Collections.Generic;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using System;

namespace Eteczka.BE.Controllers
{
    public class KatLoginyController : Controller
    {
        private IKatLoginyService _KatLoginyService;

        public KatLoginyController(IKatLoginyService katLoginyService)
        {
            this._KatLoginyService = katLoginyService;
        }

        public ActionResult PobierzWszystkichUzytkownikow(string sessionId)
        {
            List<DaneiDetaleUzytkownika> usersWithCredentials = new List<DaneiDetaleUzytkownika>();

            bool success = false;
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    usersWithCredentials = _KatLoginyService.PobierzDaneUzytkownikow();
                }

                result = Json(new
                {
                    success,
                    usersWithCredentials
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

        public ActionResult PobierzDetaleUzytkownikow(string sessionId, string id)
        {
            bool padlWyjatek = false;
            bool success = false;
            List<KatLoginyDetale> users = new List<KatLoginyDetale>();
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    //Pobieramy
                }

                result = Json(new
                {
                    success = success
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
        [ActionName("DodajPrac")]
        public ActionResult DodajUzytkownika(string sessionId, AddKatLoginyDto user)
        {

            bool sucess = false;
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sucess = _KatLoginyService.DodajNowegoUzytkownika(user);
                }

                result = Json(new
                {
                    success = sucess
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
        [ActionName("ZmienHaslo")]
        public ActionResult ZmienHaslo(string sessionId, AddKatLoginyDto user)
        {

            bool sucess = false;
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sucess = _KatLoginyService.ZmienHaslo(user);
                }

                result = Json(new
                {
                    success = sucess
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
        [ActionName("UsunPrac")]
        public ActionResult UsunPrac(string sessionId, AddKatLoginyDto user)
        {

            bool sucess = false;
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sucess = _KatLoginyService.UsunUzytkownika(user.Identyfikator);
                }

                result = Json(new
                {
                    success = sucess
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
        public ActionResult DodajFirmeDlaUzytkownika(string sessionId, KatLoginy user, string firma)
        {


            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult UsunFirmeUzytkownika(string sessionId, KatLoginyFirmy firma)
        {
            bool sucess = false;
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sucess = this._KatLoginyService.UsunFirmeUzytkownika(firma);
                }

                result = Json(new
                {
                    success = sucess
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

        public ActionResult PobierzPracownika(string nazwa, string haslo)
        {
            KatLoginy user = _KatLoginyService.GetUserByNameAndPassword(nazwa, haslo);
            bool success = user != null;

            KatLoginyDetale userdetails = new KatLoginyDetale();
            List<KatLoginyFirmy> userCompanies = new List<KatLoginyFirmy>();
            List<string> firms = new List<string>();
            SessionDetails sesja = null;
            ActionResult result = null;
            try
            {
                if (success)
                {
                    userdetails = _KatLoginyService.GetUserDetails(user.Identyfikator.Trim());
                    userCompanies = _KatLoginyService.GetUserCompanies(user.Identyfikator.Trim());
                    if (userCompanies != null && userCompanies.Count > 0)
                    {
                        sesja = Sesja.UtworzSesje();
                        sesja.AktywnaFirma = userCompanies[0];
                        sesja.WszystkieFirmy = userCompanies;
                        firms = userCompanies.Select(detail =>
                        {
                            return detail.Firma;
                        }).ToList();
                        sesja.IsAdmin = user.IsAdmin;
                    }
                }

                result = Json(new
                {
                    sesja,
                    userdetails,
                    firms,
                    success,
                    isadmin = success ? user.IsAdmin : false
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
