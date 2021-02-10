using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MisViajes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca del Sitio.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Pagina de Contacto.";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Panel de Usuario.";

            return View();
        }

        public ActionResult Profile()
        {
            ViewBag.Message = "Panel de Usuario.";

            return View();
        }
    }
}