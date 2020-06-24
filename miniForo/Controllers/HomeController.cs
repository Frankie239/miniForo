using miniForo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miniForo.Models.DAL;
using System.Security.Cryptography.Xml;

namespace miniForo.Controllers
{
    [Authorize] //Esto hace que solamente sea posible entrar a estos metodos si estas registrado
    public class HomeController : Controller
    {
        // GET: Home
        /// <summary>
        /// This is the first method that you are redirectioned when you can succesfully sing in
        /// </summary>
        /// <returns>Void</returns>
        public ActionResult landing()
        {
            using (BlogContext db = new BlogContext())
            {
                for (int i = 0; i < db.Entry.Count(); i++)
                {
                    var title = db.Entry.Select(t => t.title).FirstOrDefault();
                    var description = db.Entry.Select(d => d.description).FirstOrDefault();
                    var text = db.Entry.Select(t => t.text).FirstOrDefault();
                    var userTag = db.Entry.Select(u => u.userId).FirstOrDefault();

                    ViewBag.title = title;
                    ViewBag.description = description;
                    ViewBag.text = text;
                    ViewBag.userTag = userTag;
                }

                    
                
            }
            //Esto es un decoy, realmente no son valores que funcionen, estan hardcodeados(por ahora)
            /*ViewBag.title = "LOREM IMPSUM"; 
            ViewBag.description = "a little description over here";
            ViewBag.text = "Probando probando probando probando probando.";
            ViewBag.userTag = "@FranGimen";*/

         
            return View();
        }
        /// <summary>
        /// This method is called when you click in a <tr>
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadEntry()
        {
            ViewBag.title = "LOREM IMPSUM"; 
            ViewBag.description ="a little description over here";
            ViewBag.text = "Probando probando probando probando probando.";
            ViewBag.userTag ="@FranGimen";

            return View();


        }
        /// <summary>
        /// This loads the view to create a new forum entry
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateEntry()
        {



            return View();
        }
        /// <summary>
        /// This method compiles all the information from the entry recently created and inserts it into the db.
        /// 
        /// </summary>
        /// <param name="entry">A EF generated object, passed into the function via HTTP POST</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateEntry(Entry entry)
        {
            HttpCookie userCookie = Request.Cookies["UserCookie"];
            entry.userId = userCookie.Value;

            using (BlogContext db = new BlogContext())
            {
                db.Entry.Add(entry);
                db.SaveChanges();
            }


            return View();
        }
    }
}