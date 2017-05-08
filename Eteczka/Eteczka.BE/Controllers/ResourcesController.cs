using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Eteczka.BE.Controllers
{
    public class ResourcesController : Controller
    {

        public ActionResult GetRestrictedResource(string fileName)
        {

            string eadRootName = ConfigurationManager.AppSettings["rootdir"];

            string eadRoot = System.Environment.GetEnvironmentVariable(eadRootName);

            string configurationPath = eadRoot + "/pliki/";

            string filepath = configurationPath + fileName;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());
            string base64PDF = System.Convert.ToBase64String(filedata, 0, filedata.Length);

            return Json(new
            {
                data = base64PDF
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
