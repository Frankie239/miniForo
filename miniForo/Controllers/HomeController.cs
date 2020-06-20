using miniForo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult CreateEntry()
        {

            return View();
        }
    }
}