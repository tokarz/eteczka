using System;
using System.Web.Mvc;
using System.Configuration;
using Eteczka.BE.Utils;
using System.IO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class ResourcesController : Controller
    {
        private PlikiUtils _PlikiUtils;

        public ResourcesController(PlikiUtils plikiUtils)
        {
            this._PlikiUtils = plikiUtils;
        }

        public ActionResult GetRestrictedResource(string sessionId, string fileName)
        {
            string base64PDF = "";
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                string firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma;
                string eadRootName = ConfigurationManager.AppSettings["rootdir"];
                string pliki = ConfigurationManager.AppSettings["filesdir"];

                string eadRoot = Environment.GetEnvironmentVariable(eadRootName);

                string filepath = System.IO.Path.Combine(eadRoot, pliki, firma,  fileName);

                base64PDF = _PlikiUtils.PobierzZaszyfrowanaZawartoscPliku(filepath);
            }


            return Json(new
            {
                data = base64PDF
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetResource(string sessionId, string fileName)
        {
            string base64PDF = "";
            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                string firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma;
                string eadRootName = ConfigurationManager.AppSettings["rootdir"];
                string eadRoot = Environment.GetEnvironmentVariable(eadRootName);
                string filepath = Path.Combine(eadRoot, "waitingroom", firma, fileName);

                base64PDF = _PlikiUtils.PobierzZaszyfrowanaZawartoscPliku(filepath);
            }

            return Json(new
            {
                data = base64PDF
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
