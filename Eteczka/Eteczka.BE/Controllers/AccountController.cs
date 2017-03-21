using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Eteczka.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult GetAccounts(string sessionID)
        {
            List<string> messages = new List<string>();
            messages.Add("test1");

            return Json(new
            {
                data = messages
            }, JsonRequestBehavior.AllowGet);
        }
    
    }
}
