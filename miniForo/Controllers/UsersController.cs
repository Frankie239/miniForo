using miniForo.Models.DAL;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Security;
using System.Web;
using System.Data.SqlClient;

namespace miniForo.Controllers
{
    public class UsersController : Controller
    {
        /// <summary>
        /// This method Encrypts the password in a MD5 HASH
        /// </summary>
        /// <param name="password">the password string to be converted to MD5 HASH</param>
        /// <returns>string</returns>
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

        /// <summary>
        /// Method that loads the page to create a new user
        /// </summary>
        /// <returns></returns>
        public ActionResult createUser()
        {

            return View();
        }

        /// <summary>
        /// This method compiles and compares all the information to create a user with almost no errors
        /// </summary>
        /// <param name="newUser"> object created by EF of user</param>
        /// <param name="emailRepeat"> a email repetition</param>
        /// <param name="password">As the password is no directly attached to the user, i used a parameter </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createUser(User newUser, string emailRepeat, string password)
        {
            Password passWordEncrypted = new Password(); //Creo un nuevo objeto password para luego insertarlo en la base de datos

            string action = "Landing";
            string controller = "Home";
            Session["message"] = null; //por default el mensaje es null para que pueda entrar y mostrarse en el momento que realmente haya alg que mostrar
            

            if (emailRepeat == newUser.email) //Si el email es igual al repetido
            {
                passWordEncrypted.userId = newUser.userTag; // le asigno el user tag al elemento pasword para insertarlo con la FK
                passWordEncrypted.password1 = Encrypt(password); //encriptando la contraseña para guardarla como hash
                newUser.creationDate = DateTime.Now.Date; //Le doy una fecha de creacion para que luego quede bonito y diga cuand se creó



                using (BlogContext db = new BlogContext())
                {
                    db.User.Add(newUser); //Agrego primero el usuario por la fk
                    db.Password.Add(passWordEncrypted); //Luego la password
                    try
                    {
                        db.SaveChanges(); //Esto esta aca porque si en el caso de que algo se rompa en el insert, no se rompa toda la app
                    }
                    catch(SqlException)
                    {
                        Session["message"] = "Something went wrong during saving your data, please try again";
                    }

                    finally
                    {
                        
                        action = "logIn"; //Le pongo estos valores a la redireccion. 
                        controller = "Users";
                        
                    }
                }

               
                //return View();
                return RedirectToAction(action, controller); //si el codigo llego hasta aca sin problemas, deberia redirigir al login
            }
            else
            {
                Session["message"] = "the two emails do not match";
                return RedirectToAction("createUser", "Users"); //Si hubo problemas, vendria para aca y de vuelta a registrase con el mensaje de error
            }

        }
        /// <summary>
        /// Log in method, this compares the email and the password(previusly encrypted) against
        /// the corresponding database tables.
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password"> user password</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string email, string password)
        {
            User user = new User();
            Password pass = new Password();
            string message = "";
            Session["userName"] = null;
            string Upass = "";
            string Uemail = "";
            

            password = Encrypt(password);

            HttpCookie userTagCk = new HttpCookie("UserCookie");

            userTagCk.Expires.AddYears(50);

            using (BlogContext db = new BlogContext())
            {

                
                try
                {

                    var usuario = db.User.Where(b => b.email == email).FirstOrDefault();
                    var passw = db.Password.Where(p => p.userId == usuario.userTag).FirstOrDefault();
                    Upass = passw.password1;

                    userTagCk.Value = usuario.userTag;

                    Uemail = usuario.email;

                }
                catch (System.Reflection.TargetException) //si no llegas a encontrar alguno de los campos que te pase aca
                {
                    Session["message"] = "Incorrect Credentials";
                    return View(); //recarga la pagina de log in
                }
               


              




            }

            if (Uemail == email)
            {
                if (Upass == password)
                {
                    FormsAuthentication.SetAuthCookie(user.email, true);
                    Response.Cookies.Add(userTagCk);
                    return RedirectToAction("Landing", "Home");
                    
                }
                else
                {
                    Session["message"] = "Incorrect Password";
                    return View();
                }

            }

            else
            {
                Session["message"] = "Incorrect Email";

                return View();



            }
            //Esto es si todo sale bien, recarga la pagina de home



        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Landing", "Home");
        }



    }

}