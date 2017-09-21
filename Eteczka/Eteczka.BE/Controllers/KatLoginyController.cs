using System.Linq;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using System.Collections.Generic;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class KatLoginyController : Controller
    {
        private IKatLoginyService _KatLoginyService;

        public KatLoginyController(IKatLoginyService katLoginyService)
        {
            this._KatLoginyService = katLoginyService;
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

    }
}
