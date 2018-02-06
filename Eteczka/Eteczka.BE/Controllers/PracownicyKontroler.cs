using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using System.Web.Script.Serialization;
using System;
using Eteczka.BE.Model;
using NLog;
using Eteczka.BE.Services;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Controllers
{
    public class PracownicyController : Controller
    {
        Logger LOGGER = LogManager.GetLogger("PracownicyController");
        private IPracownicyService _PracownicyService;
        private IImportService _ImportService;

        public PracownicyController(IPracownicyService pracownicyService, IImportService importService)
        {
            this._PracownicyService = pracownicyService;
            this._ImportService = importService;
        }

        public ActionResult PobierzWszystkich(string sessionId)
        {
            LOGGER.Info("Pobieranie pracownikow dla [" + sessionId + "]");

            List<Pracownik> pracownicy = new List<Pracownik>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pracownicy = _PracownicyService.PobierzWszystkich(sesja);
                LOGGER.Info("Pobrano pracownikow [" + (pracownicy != null ? pracownicy.Count : 0) + "]");
            }

            var result = Json(new
            {
                data = pracownicy,
                count = pracownicy != null ? pracownicy.Count : 0
            }, JsonRequestBehavior.AllowGet);

            var serializer = new JavaScriptSerializer();

            // For simplicity just use Int32's max value.
            // You could always read the value from the config section mentioned above.
            serializer.MaxJsonLength = Int32.MaxValue;

            var resultSerialized = new ContentResult
            {
                Content = serializer.Serialize(result),
                ContentType = "application/json"
            };
            return resultSerialized;
        }

        [HttpPut]
        [ActionName("Dodaj")]
        public ActionResult DodajPracownika(string sessionId, Pracownik user)
        {
            InsertResult wynikInserta = new InsertResult();
            ActionResult result = null;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    wynikInserta = _PracownicyService.DodajPracownika(user, sesja);
                }
                result = Json(new
                {
                    success = wynikInserta,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new
                {
                    success = false,
                    wyjatek = true,
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [ActionName("Edytuj")]
        public ActionResult EdytujPracownika(string sessionId, Pracownik pracownik)
        {
            InsertResult success = new InsertResult();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    success = _PracownicyService.EdytujPracownika(pracownik, sesja);
                }

                result = Json(new
                {
                    sucess = success
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

        public ActionResult PobierzWszystkichZatrudnionych(string sessionId)
        {
            List<Pracownik> pracownicy = new List<Pracownik>();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    pracownicy = _PracownicyService.PobierzWszystkichZatrudnionych(sesja);
                }
                result = Json(new
                {
                    sucess = pracownicy != null && pracownicy.Count > 0 ? true : false,
                    data = pracownicy,
                    count = pracownicy != null ? pracownicy.Count : 0
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


        public ActionResult PobierzPozostalych(string sessionId)
        {
            List<Pracownik> pracownicy = new List<Pracownik>();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    pracownicy = _PracownicyService.PobierzPozostalych(sesja);
                }

                result = Json(new
                {
                    sucess = pracownicy != null && pracownicy.Count > 0 ? true : false,
                    data = pracownicy,
                    count = pracownicy != null ? pracownicy.Count : 0
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

        public ActionResult ImportujJson(string sessionId)
        {
            bool success = false;
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    success = this._ImportService.ImportujPracownikow(sessionId).ImportSukces;
                }

                result =  Json(new
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

        public ActionResult PobierzPracownikaDlaId(string sessionId, string numeread)
        {
            Pracownik pracownik = new Pracownik();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    pracownik = _PracownicyService.PobierzPoId(numeread);
                }

                result = Json(new
                {
                    sucess = pracownik != null ? true : false,
                    pracownik = pracownik
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

        public ActionResult WyszukajPracownikow(string sessionId, string search)
        {
            List<Pracownik> pracownicy = new List<Pracownik>();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    pracownicy = _PracownicyService.ZnajdzPracownikow(search, sesja);
                }

                result =  Json(new
                {
                    sucess = pracownicy != null && pracownicy.Count > 0 ? true : false,
                    Pracownicy = pracownicy
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

        public ActionResult WyszukajPracownikowPoTekscie(string sessionId, string search)
        {
            List<Pracownik> Pracownicy = new List<Pracownik>();
            ActionResult result = null;
            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    Pracownicy = _PracownicyService.ZnajdzPracownikowPoTekscie(search, sesja);
                }

               result =  Json(new
                {
                   sucess = Pracownicy != null && Pracownicy.Count > 0 ? true : false,
                   pracownicy = Pracownicy
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                result = Json(new
                {
                    sucess = false,
                    wyjatek = true
                }, JsonRequestBehavior.AllowGet);
            }
            return result;
           
        }

        public ActionResult WyszukajZatrPracownikowPoTekscie(string sessionId, string search)
        {
            List<Pracownik> Pracownicy = new List<Pracownik>();
            ActionResult result = null;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    Pracownicy = _PracownicyService.ZnajdzZatrPracownikowPoTekscie(search, sesja);
                }
                result = Json(new
                {
                    pracownicy = Pracownicy,
                    sucess = Pracownicy != null && Pracownicy.Count > 0 ? true : false
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

        public ActionResult WyszukajPozostPracownikowPoTekscie(string sessionId, string search)
        {
            List<Pracownik> Pracownicy = new List<Pracownik>();
            ActionResult result = null;

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    Pracownicy = _PracownicyService.ZnajdzPozostPracownikowPoTekscie(search, sesja);
                }

                result = Json(new
                {
                    sucess = Pracownicy != null && Pracownicy.Count > 0 ? true : false,
                    pracownicy = Pracownicy
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
