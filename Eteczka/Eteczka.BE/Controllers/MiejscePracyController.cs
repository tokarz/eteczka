using System.Web.Mvc;
using System.Collections.Generic;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Controllers
{
    public class MiejscePracyController : Controller
    {
        private IMiejscePracyService _MiejscePracyService;

        public MiejscePracyController(IMiejscePracyService miejscePracyService)
        {
            this._MiejscePracyService = miejscePracyService;
        }

        public ActionResult MiejscePracyDlaPracownika(string sessionId, Pracownik pracownik)
        {
            List<MiejscePracyDlaPracownika> miejscaPracy = _MiejscePracyService.PobierzMiejscaPracyDlaPracownika(pracownik);
            return Json(new
            {
                MiejscaPracy = miejscaPracy
            }, JsonRequestBehavior.AllowGet);

        }

    }
}
