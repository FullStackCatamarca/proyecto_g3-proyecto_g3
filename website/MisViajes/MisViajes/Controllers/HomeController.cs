using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MisViajes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MisViajes.Models;

namespace MisViajes.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        [Authorize]
        public ActionResult Dashboard()
        {

            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";


            }

            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            ViewBag.Message = "Panel de Usuario.";
            var userId = User.Identity.GetUserId();
            ApplicationUser model = new MantenimientoUsuario().RecuperarUsuario(userId);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profile(UsuarioModel model)

        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userId = User.Identity.GetUserId();
            model.Id = userId;

            int i = new MantenimientoUsuario().ModificarPerfil(model);
                
            return RedirectToAction("../Home/Profile");
        }

        [AllowAnonymous]
        public ActionResult _BarraClima()
        {
            return View();
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult ShowSlides()
        {
            return PartialView("_Slides", db.Slides.ToList());
        }
    }
}