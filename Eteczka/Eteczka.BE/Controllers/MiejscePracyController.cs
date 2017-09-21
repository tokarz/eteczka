using System.Web.Mvc;
using System.Collections.Generic;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using Eteczka.Model.DTO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class MiejscePracyController : Controller
    {
        private IMiejscePracyService _MiejscePracyService;

        public MiejscePracyController(IMiejscePracyService miejscePracyService)
        {
            this._MiejscePracyService = miejscePracyService;
        }

        public ActionResult MiejscePracyDlaPracownika(string sessionId, string numeread)
        {
            List<MiejscePracyDlaPracownika> miejscaPracy = new List<MiejscePracyDlaPracownika>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                if (numeread != null)
                {
                    miejscaPracy = _MiejscePracyService.PobierzMiejscaPracyDlaPracownika(numeread);
                }
            }
            return Json(new
            {
                MiejscaPracy = miejscaPracy
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
