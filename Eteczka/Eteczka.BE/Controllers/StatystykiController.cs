using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;
using Eteczka.BE.Enums;

namespace Eteczka.BE.Controllers
{
    public class StatystykiController : Controller
    {
        private IStatystykiService _Service;

        public StatystykiController()
        {
            this._Service = new StatystykiService();
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
