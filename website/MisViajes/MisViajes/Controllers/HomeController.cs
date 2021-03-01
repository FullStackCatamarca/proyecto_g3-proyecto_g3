using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MisViajes.Models;

namespace MisViajes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()

        {
            
            return View();
        }

        public JsonResult ObtenerDepartamentos()
        {
            List<Departamentos> lst;
            using (misviajesEntities db = new misviajesEntities())
            {

                lst = (from d in db.Departamentos
                       select d).ToList();
            }
            return Json(lst,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public JsonResult ObtenerLocalidades(int? idDepartamento)
        {
            List<Localidades> lst;
            using (misviajesEntities db = new misviajesEntities())
            {

                lst = (from d in db.Localidades
                       where d.Departamento == idDepartamento
                       select d).ToList();
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
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