using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;
using Eteczka.BE.Model;
using Eteczka.DB.Entities;

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
            KatLoginyDetale userDetails = null;
            bool success = user != null;
            if (success)
            {
                userDetails = _KatLoginyService.GetUserDetails(user.Identyfikator);
            }

            return Json(new
            {
                userdetails = userDetails,
                success = success,
                isadmin = user.IsAdmin
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
