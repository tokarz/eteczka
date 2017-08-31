using System.Web.Mvc;
using Eteczka.BE.DTO;
using System.Collections.Generic;
using Eteczka.BE.Services;
using Eteczka.DB.DTO;

namespace Eteczka.BE.Controllers
{
    public class MiejscePracyController : Controller
    {
        private IMiejscePracyService _MiejscePracyService;

        public MiejscePracyController(IMiejscePracyService miejscePracyService)
        {
            this._MiejscePracyService = miejscePracyService;
        }

        public ActionResult MiejscePracyDlaPracownika(string sessionId, PracownikDTO user)
        {
            List<MiejscePracyDlaPracownikaDto> miejscaPracy = _MiejscePracyService.PobierzMiejscaPracyDlaPracownika(user);

            return Json(new
            {
                MiejscaPracy = miejscaPracy
            }, JsonRequestBehavior.AllowGet);

        }

    }
}
