using System;
using System.Web.Mvc;
using System.Configuration;
using Eteczka.BE.Utils;
using System.IO;
using Eteczka.BE.Model;
using System.Web.Script.Serialization;

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
                string firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma.Firma.Trim();
                string eadRoot = ConfigurationManager.AppSettings["rootdir"];
                string pliki = ConfigurationManager.AppSettings["filesdir"];

                string filepath = System.IO.Path.Combine(eadRoot, pliki, firma, fileName != null ? fileName.Trim() : "");

                base64PDF = _PlikiUtils.PobierzZaszyfrowanaZawartoscPliku(filepath, sessionId);
            }

            var result = Json(new
            {
                data = base64PDF
            }, JsonRequestBehavior.AllowGet);

            var serializer = new JavaScriptSerializer();

            // For simplicity just use Int32's max value.
            // You could always read the value from the config section mentioned above.
            serializer.MaxJsonLength = Int32.MaxValue;

            var resultSerialized = new ContentResult
            {
                Content = serializer.Serialize(result),
                ContentType = "application/json"
            };
            return resultSerialized;
        }

        public ActionResult GetResource(string sessionId, string fileName)
        {
            string base64PDF = "";
            StanSesji stanSesji = Sesja.PobierzStanSesji();
            if (stanSesji.CzySesjaJestOtwarta(sessionId))
            {
                string firma = Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma.Firma;
                string eadRoot = ConfigurationManager.AppSettings["rootdir"];
                string userFolder = stanSesji.PobierzSesje(sessionId).UserWaitingroom;
                string filepath = Path.Combine(eadRoot, "waitingroom", firma, userFolder, fileName);
                base64PDF = _PlikiUtils.PobierzZaszyfrowanaZawartoscPliku(filepath, sessionId);
            }

            var result = Json(new
            {
                data = base64PDF
            }, JsonRequestBehavior.AllowGet);

            var serializer = new JavaScriptSerializer();

            // For simplicity just use Int32's max value.
            // You could always read the value from the config section mentioned above.
            serializer.MaxJsonLength = Int32.MaxValue;

            var resultSerialized = new ContentResult
            {
                Content = serializer.Serialize(result),
                ContentType = "application/json"
            };
            return resultSerialized;
        }


    }
}
