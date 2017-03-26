using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Controllers
{
    public class UtilsController : Controller
    {
        public ActionResult IsPeselValid(string pesel, string plec)
        {
            bool success = false;
            success = new Osys().SprawdzPesel(plec, pesel);

            return Json(new
            {
                valid = success
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
