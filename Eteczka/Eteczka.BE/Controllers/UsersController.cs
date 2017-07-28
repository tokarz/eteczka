using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eteczka.BE.DTO;
using Eteczka.BE.Services;
using Eteczka.BE.Model;

namespace Eteczka.BE.Controllers
{
    public class UsersController : Controller
    {
        private IUsersService _UserService;

        public UsersController(IUsersService userService)
        {
            this._UserService = userService;
        }

        public ActionResult PobierzPracownika(string nazwa, string haslo)
        {
            List<UserDto> user = _UserService.GetUserByNameAndPassword(nazwa, haslo);
            bool success = user.Count > 0;
            string newSession = "";
            if(success)
            {
                newSession = Sesja.UtworzSesje();
            }

            return Json(new
            {
                user = user,
                success = success,
                sessionId = newSession
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
