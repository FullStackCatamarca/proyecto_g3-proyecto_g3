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
using Microsoft.AspNet.Identity.EntityFramework;

namespace MisViajes.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerDepartamentos()
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<Departamentos> lst = db.Departamentos.ToList<Departamentos>();

            return Json(lst,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerLocalidades(int? idDepartamento)
        {
            
            List<Localidades> lst = new List<Localidades>();
            
            db.Configuration.LazyLoadingEnabled = false;
            if (idDepartamento != null)
                lst = db.Localidades.Where(l => l.DepartamentoId == idDepartamento).ToList<Localidades>();
            
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

            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            model.Id = userId;
            model.UserName = user.UserName;
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

        public ActionResult Privacidad()
        {
            ViewBag.Message = "Privacidad.";

            return View();
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult Counter()
        {
            ViewBag.Rutas = db.Rutas.Count().ToString();
            ViewBag.Usuarios = db.Users.Count().ToString();
            int hospedajes = db.Servicios.Where(x => x is Hospedajes).Count();
            ViewBag.Hospedajes = hospedajes.ToString();
            ViewBag.Servicios = (db.Servicios.Count() - hospedajes).ToString();

            return PartialView("Counter");
        }
    }

}