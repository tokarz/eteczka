using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;

namespace Eteczka.BE.Controllers
{
    public class WydzialyController: Controller
    {
        private IWydzialyService _wydzialyService;

        public WydzialyController (IWydzialyService wydzialyService)
        {
            this._wydzialyService = wydzialyService;
        }

        public ActionResult PobierzWydzialy(string firma)
        {

            List<WydzialDTO> PobraneWydzialy = _wydzialyService.PobierzWydzialyDlaFirmy(firma);

            return Json(new
            {
                Wydzialy = PobraneWydzialy
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
