﻿using System.Linq;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using System.Collections.Generic;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using System;
using NLog;
using Eteczka.Utils.Logger;
using Eteczka.Utils.Common.DTO;

namespace Eteczka.BE.Controllers
{
    public class KatLoginyController : Controller
    {
        IEadLogger LOGGER = LoggerFactory.GetLogger();
        private IKatLoginyService _KatLoginyService;

        public KatLoginyController(IKatLoginyService katLoginyService)
        {
            this._KatLoginyService = katLoginyService;
        }

        public ActionResult SprawdzHasloKrotkie(string sessionId, string password)
        {
            bool success = false;
            ActionResult result = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails daneSesji = stanSesji.PobierzSesje(sessionId);
                    success = _KatLoginyService.SprawdzHasloKrotkie(daneSesji.IdUzytkownika, password);
                }

                result = Json(new
                {
                    success,
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
                    success
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

        [HttpPost]
        public ActionResult AktualizujFirmeDlaUzytkownika(string sessionId, KatLoginyFirmy company)
        {
            bool sucess = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (true || stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sesja = stanSesji.PobierzSesje(sessionId);
                    sucess = _KatLoginyService.AktualizujFirmeDlaUzytkownika(company);
                }

                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);
                LOGGER.LOG_DANE_OSOBOWE(PoziomLogowania.INFO, Akcja.EMPLOYEE_PERMISSIONS_EDIT, sesja, "KatLoginyFirmy", company);
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
        public ActionResult DodajFirmeDlaUzytkownika(string sessionId, KatLoginyFirmy company)
        {
            bool sucess = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (true || stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sesja = stanSesji.PobierzSesje(sessionId);
                    sucess = _KatLoginyService.DodajFirmeDlaUzytkownika(company);
                }

                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);
                LOGGER.LOG_DANE_OSOBOWE(PoziomLogowania.INFO, Akcja.EMPLOYEE_PERMISSIONS_ADD, sesja, "KatLoginyFirmy", company);
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
            SessionDetails sesja = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sesja = stanSesji.PobierzSesje(sessionId);
                    sucess = _KatLoginyService.DodajNowegoUzytkownika(user);
                }

                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);

                LOGGER.LOG_DANE_OSOBOWE(PoziomLogowania.INFO, Akcja.USER_PERSONAL_DATA_ADD, sesja, "KatLoginy, KatLoginyDetale", user);
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

        public ActionResult ZmienHasloAdministratora(string sessionId, string oldPassword, string shortPassword, string longPassword)
        {
            bool sucess = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sesja = stanSesji.PobierzSesje(sessionId);
                    sucess = _KatLoginyService.ZmienHasloAdministratora(shortPassword, longPassword);
                }

                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);


                LOGGER.LOG_DANE_OSOBOWE(PoziomLogowania.INFO, Akcja.ADMIN_PASSWORD_CHANGE, sesja, "KatLoginy", "Admin's password changed.");
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
        [ActionName("EdytujPrac")]
        public ActionResult EdytujPracownika(string sessionId, AddKatLoginyDto user)
        {
            bool sucess = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sesja = stanSesji.PobierzSesje(sessionId);
                    sucess = _KatLoginyService.EdytujUzytkownika(user);
                }

                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);
                LOGGER.LOG_DANE_OSOBOWE(PoziomLogowania.INFO, Akcja.USER_PERSONAL_DATA_EDIT, sesja,"KatLoginy",user);
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
        [ActionName("ZmienHasloShort")]
        public ActionResult ZmienHasloShort(string sessionId, AddKatLoginyDto user)
        {
            ActionResult result = null;
            bool sucess = false;

            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId))
                {
                    sucess = _KatLoginyService.ZmienHasloShort(user);
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
        public ActionResult UsunPrac(string sessionId, KatLoginyDetale user)
        {

            bool sucess = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sesja = stanSesji.PobierzSesje(sessionId);
                    sucess = _KatLoginyService.UsunUzytkownika(user.Identyfikator);
                }

                result = Json(new
                {
                    success = sucess
                }, JsonRequestBehavior.AllowGet);

                LOGGER.LOG_DANE_OSOBOWE(PoziomLogowania.INFO, Akcja.EMPLOYEE_PERSONAL_DATA_DELETE, sesja, "KatLoginyDetale", user);
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

        public ActionResult PobierzPracownika(string nazwa, string haslo)
        {
            LOGGER.LOG(PoziomLogowania.INFO, Akcja.ZWYKLA, string.Format("Proba logowania [{0}]", nazwa));
            KatLoginy user = _KatLoginyService.GetUserByNameAndPassword(nazwa, haslo);
            bool success = user != null;

            KatLoginyDetale userdetails = new KatLoginyDetale();
            List<KatLoginyFirmy> userCompanies = new List<KatLoginyFirmy>();
            List<string> firms = new List<string>();
            List<string> AktywnyFolder = new List<string>();
            SessionDetails sesja = null;
            ActionResult result = null;
            try
            {
                if (success)
                {
                    userdetails = _KatLoginyService.GetUserDetails(user.Identyfikator.Trim());
                    userCompanies = _KatLoginyService.GetUserCompanies(user.Identyfikator.Trim());

                    sesja = Sesja.UtworzSesje();
                    sesja.IdUzytkownika = user.Identyfikator.Trim();

                    if (user.IsAdmin == true)
                    {
                        sesja.IsAdmin = true;
                    }
                    else
                    {
                        if (userCompanies != null && userCompanies.Count > 0)
                        {
                            sesja.AktywnaFirma = userCompanies[0];
                            sesja.WszystkieFirmy = userCompanies;
                            firms = userCompanies.Select(detail =>
                            {
                                return detail.Firma;
                            }).ToList();
                            sesja.IsAdmin = false;
                            sesja.AktywnyFolder = AktywnyFolder;
                        }
                    }

                    LOGGER.LOG(PoziomLogowania.STATISTIC, Akcja.ZWYKLA, string.Format("Zalogowano", nazwa), sesja);
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
