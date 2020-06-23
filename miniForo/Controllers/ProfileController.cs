using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Security;

namespace miniForo.Controllers
{
    [Authorize] //Esto hace que solo se pueda entrar aca si es que estas logueado
    public class ProfileController : Controller
    {
        // GET: Profile
        /// <summary>
        /// Aca va todo lo que se hace con el perfil, editar cosas del perfil y demas.
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyProfile()
        {
            throw new NotImplementedException();

            return View();
        }
      
    }
}