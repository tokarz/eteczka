using System;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mime;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Controllers
{
    public class ResourcesController : Controller
    {
        private PlikiUtils _PlikiUtils;

        public ResourcesController(PlikiUtils plikiUtils)
        {
            this._PlikiUtils = plikiUtils;
        }

        public ActionResult GetRestrictedResource(string fileName)
        {
            string eadRootName = ConfigurationManager.AppSettings["rootdir"];
            string pliki = ConfigurationManager.AppSettings["filesdir"];

            string eadRoot = Environment.GetEnvironmentVariable(eadRootName);

            string filepath = System.IO.Path.Combine(eadRoot, pliki, fileName);

            string base64PDF = _PlikiUtils.PobierzZaszyfrowanaZawartoscPliku(filepath);
           

            return Json(new
            {
                data = base64PDF
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
