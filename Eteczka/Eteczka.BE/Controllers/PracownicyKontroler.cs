using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using System.Web.Script.Serialization;
using System;
using Eteczka.BE.Model;
using NLog;

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

        public ActionResult PobierzWszystkichZatrudnionych(string sessionId)
        {
            List<Pracownik> pracownicy = new List<Pracownik>(); 
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pracownicy = _PracownicyService.PobierzWszystkichZatrudnionych(sesja);
            }
            return Json(new
            {
                data = pracownicy,
                count = pracownicy != null ? pracownicy.Count : 0
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PobierzPozostalych(string sessionId)
        {

            List<Pracownik> pracownicy = new List<Pracownik>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pracownicy = _PracownicyService.PobierzPozostalych(sesja);
            }
            

            return Json(new
            {
                data = pracownicy,
                count = pracownicy != null ? pracownicy.Count : 0
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportujJson(string sessionId)
        {
            bool success = false;

            success = this._ImportService.ImportujPracownikow(sessionId).ImportSukces;

            return Json(new
            {
                success = success
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzPracownikaDlaId(string numeread)
        {
            Pracownik pracownik = _PracownicyService.PobierzPoId(numeread);

            return Json(new
            {
                pracownik = pracownik
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult WyszukajPracownikow(string search)
        {
            List<Pracownik> Pracownicy = _PracownicyService.ZnajdzPracownikow(search);
            return Json(new
            {
                Pracownicy = Pracownicy
            }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult WyszukajPracownikowPoTekscie(string search, string sessionId)
        {

            List<Pracownik> Pracownicy = new List<Pracownik>();
             if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
             {
                 SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                 Pracownicy = _PracownicyService.ZnajdzPracownikowPoTekscie(search, sesja);
             }

           
            return Json(new
            {
                pracownicy = Pracownicy
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WyszukajZatrPracownikowPoTekscie(string search, string sessionId)
        {

            List<Pracownik> Pracownicy = new List<Pracownik>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                Pracownicy = _PracownicyService.ZnajdzZatrPracownikowPoTekscie(search, sesja);
            }
            return Json(new
            {
                pracownicy = Pracownicy
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WyszukajPozostPracownikowPoTekscie(string search, string sessionId)
        {
           List<Pracownik> Pracownicy = new List<Pracownik>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                Pracownicy = _PracownicyService.ZnajdzPozostPracownikowPoTekscie(search, sesja);
            }
            //List<Pracownik> Pracownicy = _PracownicyService.ZnajdzPozostPracownikowPoTekscie(search);
            return Json(new
            {
                pracownicy = Pracownicy
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
