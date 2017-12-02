using System.Linq;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using System.Collections.Generic;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Controllers
{
    public class KatLoginyController : Controller
    {
        private IKatLoginyService _KatLoginyService;

        public KatLoginyController(IKatLoginyService katLoginyService)
        {
            this._KatLoginyService = katLoginyService;
        }

        [HttpPost]
        [ActionName("DodajPrac")]
        public ActionResult DodajUzytkownika(string sessionId, AddKatLoginyDto user)
        {

            bool result = false;
            StanSesji stanSesji = Sesja.PobierzStanSesji();
            if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
            {

            }

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
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
            bool result = false;
            StanSesji stanSesji = Sesja.PobierzStanSesji();
            if (stanSesji.CzySesjaJestOtwarta(sessionId) && stanSesji.CzySesjaAdministratora(sessionId))
            {

                result = this._KatLoginyService.UsunFirmeUzytkownika(user, firma);
            }

            return Json(new
            {
                success = result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzPracownika(string nazwa, string haslo)
        {
            KatLoginy user = _KatLoginyService.GetUserByNameAndPassword(nazwa, haslo);
            List<KatLoginyDetale> userDetails = new List<KatLoginyDetale>();
            List<string> firmy = new List<string>();
            SessionDetails sesja = null;
            bool success = user != null;

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

            return Json(new
            {
                sesja = sesja,
                userdetails = userDetails.Count == 0 ? null : userDetails[0],
                firms = firmy,
                success = success,
                isadmin = success ? user.IsAdmin : false
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PobierzWszystkichPracownikow(string sessionId)
        {
            List<PracownicyZFirmamiDTO> users = new List<PracownicyZFirmamiDTO>();

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

            return Json(new
            {
                users = users
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
