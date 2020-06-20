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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult landing()
        {
            ViewBag.title = "LOREM IMPSUM";
            ViewBag.description = "a little description over here";
            ViewBag.text = "Probando probando probando probando probando.";
            ViewBag.userTag = "@FranGimen";

         
            return View();
        }

        public ActionResult LoadEntry()
        {
            ViewBag.title = "LOREM IMPSUM";
            ViewBag.description ="a little description over here";
            ViewBag.text = "Probando probando probando probando probando.";
            ViewBag.userTag ="@FranGimen";

            return View();


        }
        [HttpGet]
        public ActionResult CreateEntry()
        {



            return View();
        }

        [HttpPost]
        public ActionResult CreateEntry(Entry entry)
        {
            entry.userId = "@FranGimen";

            using (BlogContext db = new BlogContext())
            {
                db.Entry.Add(entry);
                db.SaveChanges();

            }


            return View();
        }
    }
}