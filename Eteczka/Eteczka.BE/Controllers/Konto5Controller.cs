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

        public ActionResult PobierzKonta5(string sessionId)
        {

            List<KatKonto5> pobraneKonta5 = new List<KatKonto5>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pobraneKonta5 = _katKonto5Service.PobierzKonta5(sesja);
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


            
    }
}
