using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.Model.Entities;
using Eteczka.BE.Services;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class PodWydzialController : Controller
    {
        private IPodWydzialService _PodWydzialService;
        
        public PodWydzialController (IPodWydzialService PodWydzialService)
        {
            this._PodWydzialService = PodWydzialService;
        }

        public ActionResult PobierzWszystkiePodwydzialy(string sessionId, string wydzial)
        {

            List<KatPodWydzialy> pobranePodWydzialy = new List<KatPodWydzialy>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pobranePodWydzialy = _PodWydzialService.PobranaListaPodWydzialow(sesja, wydzial);
            }
            
            return Json(new
            {
                PodWydzialy = pobranePodWydzialy
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
