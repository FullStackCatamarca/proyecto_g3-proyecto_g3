using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MisViajes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MisViajes.Controllers
{

    public class RutasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Rutas
        public async Task<ActionResult> Index()
        {
            
            if (User.IsInRole("Staff") || User.IsInRole("Administrador")) 
                // Lista Completa
                return View(await db.Rutas.ToListAsync());

            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var user = UserManager.FindById(User.Identity.GetUserId());
            
            // Lista Solo las del usuario
            return View(await db.Rutas.Where(x => x.User.Id == user.Id).ToListAsync());

        }

        // GET: Rutas/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var userId = (User.Identity.GetUserId());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutas rutas = await db.Rutas.FindAsync(id);

            if ((rutas != null) && (User.IsInRole("Staff") || User.IsInRole("Administrador") || (rutas.User.Id != userId)))
            {
                return View(rutas);
            }

            return HttpNotFound();

        }

        // GET: Rutas/Show/5
        [Authorize]
        public async Task<ActionResult> Show(int? id)
        {
            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";
            }

            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var userId = (User.Identity.GetUserId());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            List<Waypoint> wps = db.Waypoints.Where(w => w.rutaId == id).ToList();
            List<Servicios> srvs = new List<Servicios>();
            List<string> destinos = new List<string>();

            foreach (Waypoint wp in wps)
            {
                // todo: if servicio is available
                var srv = db.Servicios.Find(wp.ServiciosId);
                srvs.Add(srv);
                destinos.Add(srv.Nombre);
            }

            Rutas ruta = await db.Rutas.FindAsync(id);
            
            ViewBag.Nombre = ruta.Nombre;
            ViewBag.Destinos = destinos;

            return View(srvs.AsQueryable());

        }

        // GET: Rutas/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";
            }

            Rutas model = new Rutas();
            model.Abierto = true;
            return View(model);
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Abierto,Publico")] Rutas rutas)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);

            rutas.Aprobado = true;
            rutas.User = user;


            ModelState.Remove("User");
            ModelState.Remove("Aprobado");

            if (ModelState.IsValid)
            {
                db.Rutas.Add(rutas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rutas);
        }

        // GET: Rutas/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutas rutas = await db.Rutas.FindAsync(id);

            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var userId = (User.Identity.GetUserId());

            if ((rutas != null) && (User.IsInRole("Staff") || User.IsInRole("Administrador") || (rutas.User.Id != userId)))

                return View(rutas);


            return HttpNotFound();
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Abierto,Publico,Aprobado")] Rutas rutas)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            Rutas rutao = db.Rutas.AsNoTracking().Single(x => x.Id.Equals(rutas.Id));

            rutas.User = user;

            if (!(User.IsInRole("Staff") || User.IsInRole("Administrador")))
            {
                rutas.Aprobado = rutao.Aprobado;
                ModelState.Remove("Aprobado");
            }


            ModelState.Remove("User");


            ModelState.Clear();

            if (ModelState.IsValid)
            {
                db.Entry(rutas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rutas);
        }

        // GET: Rutas/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutas rutas = await db.Rutas.FindAsync(id);

            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var userId = (User.Identity.GetUserId());

            if ((rutas != null) && (User.IsInRole("Staff") || User.IsInRole("Administrador") || (rutas.User.Id != userId)))
                return View(rutas);

            return HttpNotFound();
        }

        // POST: Rutas/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rutas rutas = await db.Rutas.FindAsync(id);
            db.Rutas.Remove(rutas);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
