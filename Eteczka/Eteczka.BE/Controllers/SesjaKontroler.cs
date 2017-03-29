﻿using System;
using System.Web.Mvc;

namespace Eteczka.BE.Controllers
{
    public class SesjaController : Controller
    {

        public ActionResult CreateSessionForToken(string token)
        {
            string session = "@_" + new Random().Next() + "";

            return Json(new
            {
                session = session
            }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
