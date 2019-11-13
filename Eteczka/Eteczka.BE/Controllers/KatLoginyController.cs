using System.Linq;
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

            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.USER_PERMISSIONS_EDIT, sesja, sucess, "KatLoginyFirmy", company, " ", "User [" + company.Identyfikator.Trim() + "]" + (sucess ? " company updated" : " company update failure."));
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
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.USER_PERMISSIONS_ADD, sesja, sucess, "KatLoginyFirmy", company, " ", "User [" + company.Identyfikator.Trim() + "]" + " company " + company.Firma.Trim() + (sucess ? " added." : " add attempt failure."));
            return result;
        }

        [HttpPost]
        [ActionName("DodajPrac")]
        public ActionResult DodajUzytkownika(string sessionId, AddKatLoginyDto user)
        {

            bool sucess = false;
            ActionResult result = null;
            SessionDetails sesja = null;
            bool hasPermission = false;
            try
            {
                StanSesji stanSesji = Sesja.PobierzStanSesji();
                if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
                {
                    sesja = stanSesji.PobierzSesje(sessionId);
                    hasPermission = sesja.AktywnaFirma.Uprawnienia.RolaAddPracownik ? true : false;
                    if (hasPermission)
                    {
                        sucess = _KatLoginyService.DodajNowegoUzytkownika(user);
                        
                        result = Json(new
                        {
                            success = sucess
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result = Json(new
                        {
                            sucess = false,
                            noPermission = true
                        }, JsonRequestBehavior.AllowGet);
                    }
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
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.USER_PERSONAL_DATA_ADD, sesja, sucess, "KatLoginy, KatLoginyDetale", user, " ", "User [" + user.Identyfikator.Trim() + "]" + (sucess ? " added." : " add attempt failure."));
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
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.ADMIN_PASSWORD_CHANGE, sesja, sucess, "KatLoginy", " ", " ", (sucess ? "Admin's password changed." : "Admin's password change attemtp failure."));
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
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.USER_PERSONAL_DATA_EDIT, sesja, sucess, "KatLoginy", user, " ", sucess ? "User " + "[" + user.Identyfikator.Trim() + "]" + " edition successful." : "User " + "[" + user.Identyfikator.Trim() + "]" + " edition attempt failure.");
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
                    sucess = _KatLoginyService.UsunUzytkownikowiWszystkieFirmy(user.Identyfikator);
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
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.USER_PERSONAL_DATA_DELETE, sesja, sucess, "KatLoginyDetale", user, " ", "User " + "[" + user.Identyfikator.Trim() + "]" + (sucess ? " was removed" : " removal attempt failure."));
            return result;

        }

        [HttpPost]
        public ActionResult UsunFirmeUzytkownika(string sessionId, KatLoginyFirmy firma)
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
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.USER_PERMISSIONS_EDIT, sesja, sucess, "KatLoginyFirmy", firma, "User " + "[" + firma.Identyfikator.Trim() + "]" + firma.Firma.Trim() + (sucess ? " was removed" : " removal attempt failure."));
            return result;

        }

        public ActionResult PobierzPracownika(string nazwa, string haslo)
        {
            LOGGER.LOG(PoziomLogowania.STATISTIC, Akcja.USER_LOGIN, string.Format("Login attempt [{0}]", nazwa));

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
            LOGGER.LOG_MAIN_LOG(PoziomLogowania.INFO, Akcja.USER_LOGIN, sesja, success, "KatLoginy, KatLoginyDetale, KatLoginyFirmy", " ", " ", "User " + "[" + nazwa + "]" + (success ? " logged." : " loggin attempt failure."));
            return result;
        }

    }
}
