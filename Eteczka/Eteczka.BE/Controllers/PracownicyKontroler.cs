using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using System.Web.Script.Serialization;
using System;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class PracownicyController : Controller
    {
        private IPracownicyService _PracownicyService;
        private IImportService _ImportService;

        public PracownicyController(IPracownicyService pracownicyService, IImportService importService)
        {
            this._PracownicyService = pracownicyService;
            this._ImportService = importService;
        }

        public ActionResult PobierzWszystkich(string sessionId)
        {
            /*
                 (YO MICHAL!)
                 1)Od teraz autoryzacja sesji przebiega tak: 
                  if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                 2) Klasa Eteczka.BE.Model.Sesja jest globalna i zawiera liste otwartycz sesji
                 3) Zmiany w tej klasie sa bardzo grozne! lepiej sie z nimi konsultujmy
                 4) Kontroler po autoryzacji sesji za pomoca operacji:
                    SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
             
                 pobiera obiekt sesji : SessionDetails (zawiera np KatLoginyDetale) 
                    ten obiekt przekatujemy dalej do serwisow i innych hefalumpow aby operaowac na 
                    firmach, uzyszkodnikach itd itp.
             */


            List<Pracownik> pracownicy = new List<Pracownik>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pracownicy = _PracownicyService.PobierzWszystkich(sesja);
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

        public ActionResult PobierzWszystkichZatrudnionych(string sessionmId)
        {
            List<Pracownik> pracownicy = _PracownicyService.PobierzWszystkichZatrudnionych();

            return Json(new
            {
                data = pracownicy,
                count = pracownicy != null ? pracownicy.Count : 0
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PobierzPozostalych(string sessionmId)
        {
            List<Pracownik> pracownicy = _PracownicyService.PobierzPozostalych();

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

        public ActionResult WyszukajPracownikowPoTekscie(string search)
        {
            List<Pracownik> Pracownicy = _PracownicyService.ZnajdzPracownikowPoTekscie(search);
            return Json(new
            {
                pracownicy = Pracownicy
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WyszukajZatrPracownikowPoTekscie(string search)
        {
            List<Pracownik> Pracownicy = _PracownicyService.ZnajdzZatrPracownikowPoTekscie(search);
            return Json(new
            {
                pracownicy = Pracownicy
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WyszukajPozostPracownikowPoTekscie(string search)
        {
            List<Pracownik> Pracownicy = _PracownicyService.ZnajdzPozostPracownikowPoTekscie(search);
            return Json(new
            {
                pracownicy = Pracownicy
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
