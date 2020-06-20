using miniForo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace miniForo.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users

        public ActionResult createUser()
        {

            return View();
        }

        [HttpPost]
        public ActionResult createUser(dbDummyUser user)
        {
            return View();
        }

        public ActionResult LogIn()
        {

            //Esto es si todo sale bien, recarga la pagina de home
            return RedirectToAction("Landing", "Home");
        }
    }

}