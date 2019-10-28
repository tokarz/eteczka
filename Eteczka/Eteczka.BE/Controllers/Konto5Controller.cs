using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using System;

namespace Eteczka.BE.Controllers
{
    public class Konto5Controller : Controller
    {
        private IKonto5Service _katKonto5Service;

        public Konto5Controller (IKonto5Service katKonto5Service)
        {
            this._katKonto5Service = katKonto5Service;
        }
        [HttpGet]
        public ActionResult PobierzKonta5(string sessionId)
        {

            List<KatKonto5> pobraneKonta5 = new List<KatKonto5>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pobraneKonta5 = _katKonto5Service.PobierzKonta5(sesja.AktywnaFirma.Firma);
            }
           
            return Json(new
            {
               pobraneKonta5 = pobraneKonta5
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult PobierzKonta5(string sessionId, string firma)
        {

            List<KatKonto5> pobraneKonta5 = new List<KatKonto5>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pobraneKonta5 = _katKonto5Service.PobierzKonta5(firma);
            }

            return Json(new
            {
                pobraneKonta5 = pobraneKonta5
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DodajKonto5WFirmie(string sessionId, KatKonto5 konto)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _katKonto5Service.DodajKonto5(konto, sesja.IdUzytkownika, sesja.IdUzytkownika);
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
        public ActionResult EdytujKonto5 (string sessionId, KatKonto5 konto)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _katKonto5Service.EdytujKonto5(konto, sesja.IdUzytkownika, sesja.IdUzytkownika);
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
        public ActionResult UsunKonto5(string sessionId, KatKonto5 konto)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            InsertResult sucess = new InsertResult();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    sucess = _katKonto5Service.UsunKonto5(konto, sesja.IdUzytkownika, sesja.IdUzytkownika);
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
        public ActionResult WyszukajKonto5(string sessionId, string firma, string search)
        {
            ActionResult result = null;
            SessionDetails sesja = null;
            List<KatKonto5> WyszukaneKonta = new List<KatKonto5>();

            try
            {
                if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
                {
                    sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                    WyszukaneKonta = _katKonto5Service.WyszukajKonto5(firma, search);
                }

                result = Json(new
                {
                    sucess = WyszukaneKonta != null && WyszukaneKonta.Count > 0 ? true : false,
                    wyszukaneKonta = WyszukaneKonta
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
