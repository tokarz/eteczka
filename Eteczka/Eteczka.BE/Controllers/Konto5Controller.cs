using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.Services;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class Konto5Controller : Controller
    {
        private IKonto5Service _katKonto5Service;

        public Konto5Controller (IKonto5Service katKonto5Service)
        {
            this._katKonto5Service = katKonto5Service;
        }

        public ActionResult PobierzKonta5(string sessionId)
        {

            List<KatKonto5> pobraneKonta5 = new List<KatKonto5>();
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                SessionDetails sesja = Sesja.PobierzStanSesji().PobierzSesje(sessionId);
                pobraneKonta5 = _katKonto5Service.PobierzKonta5(sesja);
            }
           
            return Json(new
            {
               pobraneKonta5 = pobraneKonta5
            }, JsonRequestBehavior.AllowGet);
        }

            
    }
}
