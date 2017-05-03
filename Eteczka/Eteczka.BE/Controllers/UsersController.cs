using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;

namespace Eteczka.BE.Controllers
{
    public class UsersController : Controller
    {

        public ActionResult PobierzPracownika(string nazwa, string haslo)
        {
            UserDto user = new UsersService().GetUserByNameAndPassword(nazwa, haslo);

            return Json(new
            {
                user = user
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
