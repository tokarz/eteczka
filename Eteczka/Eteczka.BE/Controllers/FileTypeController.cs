using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eteczka.BE.Enums;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class FileTypeController : Controller
    {

        public ActionResult GetFilesType(string sessionId)
        {
            List<FileType> types = new List<FileType>();
            List<string> result = new List<string>();

            if (Sesja.PobierzStanSesji().CzySesjaJestOtwarta(sessionId))
            {
                types = Enum.GetValues(typeof(FileType)).Cast<FileType>().ToList();
                result = new List<string>();
                foreach (FileType type in types)
                {
                    result.Add(type.ToString());
                }
            }

            return Json(new
            {
                orderedTypes = types,
                types = result
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
