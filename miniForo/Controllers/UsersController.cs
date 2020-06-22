using miniForo.Models.DAL;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Security;

namespace miniForo.Controllers
{
    public class UsersController : Controller
    {
        public string Encrypt(string password)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            password = hash.ToString();

            return password;
        }
        // GET: Users

        public ActionResult createUser()
        {

            return View();
        }

        [HttpPost]
        //Añadido del metodo de creacion de usuarios
        //Pero falta hacer que se validen bien y que tire un error.
        public ActionResult createUser(User newUser, string emailRepeat, string password)
        {
            Password passWordEncrypted = new Password();

            string action = "Landing";
            string controller = "Home";
            ViewBag.message = "User successfully registered";
            ViewData["state"] = true;

            if (emailRepeat == newUser.email)
            {
                passWordEncrypted.userId = newUser.userTag;
                passWordEncrypted.password1 = Encrypt(password); //encriptando la contraseña para guardarla como hash
                newUser.creationDate = DateTime.Now.Date;


                using (BlogContext db = new BlogContext())
                {
                    db.User.Add(newUser);
                    db.Password.Add(passWordEncrypted);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch(Exception)
                    {
                        ViewBag.message = "Something went wrong during saving your data, please try again";
                    }

                    finally
                    {
                        action = "CreateUser";
                        controller = "Users";
                        
                    }
                }
                //return View();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewBag.message = "the two emails do not match";
                return RedirectToAction("createUser", "Users");
            }

        }
        /* Log in method(Not working)*/
        [HttpPost]
        public ActionResult LogIn(string email, string password)
        {
            User user = new User();
            Password pass = new Password();
            string message = "";
            ViewBag.message = message;
            string Upass = "";
            string Uemail = "";

            password = Encrypt(password);


            using (BlogContext db = new BlogContext())
            {

                var usuario = db.User.Where(b => b.email == email).FirstOrDefault();
                var passw = db.Password.Where(p => p.userId == usuario.userTag).FirstOrDefault();

                Upass = passw.password1;
                Uemail = usuario.email;





            }
         


            if(Uemail == email)
            {
                if(Upass == password)
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