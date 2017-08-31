using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eteczka.BE.Enums;

namespace Eteczka.BE.Controllers
{
    public class FileTypeController : Controller
    {

        public ActionResult GetFilesType(string sessionID)
        {
            List<FileType> types = Enum.GetValues(typeof(FileType)).Cast<FileType>().ToList();
            List<string> result = new List<string>();
            foreach(FileType type in types)
            {
                result.Add(type.ToString());
            }

            return Json(new
            {
                orderedTypes = types,
                types = result
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
