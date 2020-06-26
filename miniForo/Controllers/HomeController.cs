using miniForo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miniForo.Models.DAL;
using System.Security.Cryptography.Xml;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Web.UI;

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
            ListingEntry entrys = new ListingEntry();
            try
            {
                using (BlogContext db = new BlogContext())
                {

                    

                    entrys.Listing = db.Entry.SqlQuery("SELECT * FROM [Entry]").ToList();
                  


                }

            }

            catch (Exception)
            {
                Session["message"] = "Theres no entrys to load";
            }
            
            

         
            return View(entrys);
        }
        /// <summary>
        /// This method is called when you click in a <tr>
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadEntry(int entryId)
        {
            
            using(BlogContext db = new BlogContext())
            {
                try
                {
                    Entry found = db.Entry.Find(entryId);

                    ViewBag.title = found.title.ToString();
                    ViewBag.description = found.description.ToString();
                    ViewBag.text = found.text.ToString();
                    ViewBag.userTag = found.userId.ToString();

                }
                catch
                {

                }



            }
        
            return View();


        }
        /// <summary>
        /// This loads the view to create a new forum entry
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateEntry()
        {

            if (Request.Cookies["UserCookie"] == null)
            {
                return RedirectToAction("LogIn", "Users");
            }

            else
            {
                return View();
            }

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


            return RedirectToAction("landing", "Home");

        }
    }
}