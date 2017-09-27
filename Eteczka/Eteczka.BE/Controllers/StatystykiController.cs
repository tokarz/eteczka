using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;
using Eteczka.BE.Enums;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class StatystykiController : Controller
    {
        private IStatystykiService _Service;

        public StatystykiController(IStatystykiService service)
        {
            this._Service = service;
        }

        public ActionResult PobierzDaneWykresowe(string sessionId)
        {
            List<DaneWykresowe> result = new List<DaneWykresowe>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                result = _Service.PobierzDaneWykresowe(TypWykresu.PIE);
            }

            return Json(new
            {
                chartdata = result
            }, JsonRequestBehavior.AllowGet);


        }

    }
}
