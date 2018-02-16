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
                    sucess = _KatLoginyService.UsunUzytkownika(user);
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
        public ActionResult UsunFirmeUzytkownika(string sessionId, KatLoginy user, string firma)
        {
            bool sucess = false;
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sucess = this._KatLoginyService.UsunFirmeUzytkownika(user, firma);
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
            List<KatLoginyDetale> userDetails = new List<KatLoginyDetale>();
            List<string> firmy = new List<string>();
            SessionDetails sesja = null;
            bool success = user != null;
            ActionResult result = null;
            try
            {
                if (success)
                {
                    userDetails = _KatLoginyService.GetUserDetails(user.Identyfikator);
                    if (userDetails != null && userDetails.Count > 0)
                    {
                        sesja = Sesja.UtworzSesje();
                        sesja.AktywnaFirma = userDetails[0].Firma;
                        sesja.AktywnyUser = userDetails[0];
                        sesja.WszystkieDetale = userDetails;
                        firmy = userDetails.Select(detail =>
                        {
                            return detail.Firma;
                        }).ToList();
                        sesja.IsAdmin = user.IsAdmin;
                    }
                }

                result = Json(new
                {
                    sesja = sesja,
                    userdetails = userDetails.Count == 0 ? null : userDetails[0],
                    firms = firmy,
                    success = success,
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

        public ActionResult PobierzWszystkichPracownikow(string sessionId)
        {
            List<PracownicyZFirmamiDTO> users = new List<PracownicyZFirmamiDTO>();
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    List<KatLoginyDetale> detale = _KatLoginyService.GetAllUsersDetails();

                    foreach (KatLoginyDetale detal in detale)
                    {
                        PracownicyZFirmamiDTO prac = users.Find(x => x.Identyfikator == detal.Identyfikator);
                        if (prac == null)
                        {
                            prac = new PracownicyZFirmamiDTO()
                            {
                                Identyfikator = detal.Identyfikator,
                                Firmy = new List<string>() { detal.Firma },
                                Confidential = detal.Confidential
                            };
                            users.Add(prac);
                        }
                        else
                        {
                            prac.Firmy.Add(detal.Firma);
                        }
                    }
                }

                result = Json(new
                {
                    sucess = users != null && users.Count > 0 ? true : false,
                    users = users
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
