using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class ResourcesController : Controller
    {

        public ActionResult GetRestrictedResource(string fileName)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Content/Restricted/" + fileName;
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
