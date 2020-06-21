using miniForo.Models;
using miniForo.Models.DAL;
using miniForo.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

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

        [HttpPost]
        public ActionResult LogIn(string email, string password)
        {
            User user = new User();
            Password pass = new Password();
            string message = "";
            ViewBag.message = message;
            



            using (BlogContext db = new BlogContext())
            {

                user = db.User.FirstOrDefault(e => e.email == email);
                //pass = db.Password.SqlQuery("SELECT password FROM [Password] WHERE [userId] = @tag", user.userTag);





            }
         


            if(user.email == email)
            {
                if(passwords == password)
                {
                    FormsAuthentication.SetAuthCookie(user.email, true);
                    return RedirectToAction("UserLogedIn", "Profile");
                }
              
            }
           
            else
            {
                message = "User or credentials not found or credentials incorrect";
                //throw new UserNotFoundException();
                

            }
            //Esto es si todo sale bien, recarga la pagina de home
            return RedirectToAction("Landing", "Home");
        }
    }

}