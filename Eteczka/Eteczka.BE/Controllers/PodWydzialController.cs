using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;

namespace Eteczka.BE.Controllers
{
    public class PodWydzialController : Controller
    {
        private IPodWydzialService _PodWydzialService;
        
        public PodWydzialController (IPodWydzialService PodWydzialService)
        {
            this._PodWydzialService = PodWydzialService;
        }

        public ActionResult PobierzWszystkiePodwydzialy()
        {
            List<KatPodWydzialy> pobranePodWydzialy = _PodWydzialService.PobranaListaPodWydzialow();
            return Json(new
            {
                PodWydzialy = pobranePodWydzialy
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
