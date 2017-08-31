using System.Collections.Generic;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;
using Eteczka.BE.Enums;

namespace Eteczka.BE.Controllers
{
    public class StatystykiController : Controller
    {
        private IStatystykiService _Service;

        public StatystykiController(IStatystykiService service)
        {
            this._Service = service;
        }

        public ActionResult PobierzDaneWykresowe()
        {
            List<DaneWykresowe> result = _Service.PobierzDaneWykresowe(TypWykresu.PIE);

            return Json(new
            {
                chartdata = result
            }, JsonRequestBehavior.AllowGet);


        }

    }
}
