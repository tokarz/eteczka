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
    public class RejonyController : Controller

    {
        private IRejonyService _rejonyService;

        public RejonyController (IRejonyService rejonyService)
        {
            this._rejonyService = rejonyService;
        }

        public ActionResult PobierzWszystkieRejony()
        {
            List<RejonDTO> PobraneRejony = _rejonyService.PobierzRejony();

            return Json(new
            {
                Rejony = PobraneRejony
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PobierzRejonyDlaWybranejFirmy(string firma)
        {
            List<RejonDTO> PobraneRejony = _rejonyService.PobierzRejonyDlaFirmy(firma);
            return Json(new
            {
                Rejony = PobraneRejony
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
