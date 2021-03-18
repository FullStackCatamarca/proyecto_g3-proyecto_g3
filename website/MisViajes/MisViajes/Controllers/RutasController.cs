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
            return View(await db.Rutas.ToListAsync());
        }

        // GET: Rutas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutas rutas = await db.Rutas.FindAsync(id);
            if (rutas == null)
            {
                return HttpNotFound();
            }
            return View(rutas);
        }

        // GET: Rutas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Abierto,Publico,Aprobado")] Rutas rutas)
        {
                this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
                var UserId = User.Identity.GetUserId();
                var user = UserManager.FindById(UserId);

                rutas.User = user;

                ModelState.Clear();

                if (ModelState.IsValid)
                {
                    db.Rutas.Add(rutas);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            return View(rutas);
        }

        // GET: Rutas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutas rutas = await db.Rutas.FindAsync(id);
            if (rutas == null)
            {
                return HttpNotFound();
            }
            return View(rutas);
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Abierto,Publico,Aprobado")] Rutas rutas)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);

            rutas.User = user;

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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rutas rutas = await db.Rutas.FindAsync(id);
            if (rutas == null)
            {
                return HttpNotFound();
            }
            return View(rutas);
        }

        // POST: Rutas/Delete/5
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
