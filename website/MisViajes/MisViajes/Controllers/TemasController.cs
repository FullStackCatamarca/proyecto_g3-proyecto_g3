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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace MisViajes.Controllers
{
    public class TemasController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Temas
        public async Task<ActionResult> Index()
        {
            return View(await db.Temas.ToListAsync());
        }

        // GET: Temas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = await db.Temas.FindAsync(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            return View(temas);
        }

        // GET: Temas/Create
        public ActionResult Create()
        {
            Temas temas = new Temas();
            temas.Activo = true;

            return View(temas);
        }

        // POST: Temas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Descripcion,Activo")] Temas temas, string returnUrl)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            temas.User = user;
            temas.Fecha = DateTime.Now;

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                db.Temas.Add(temas);
                await db.SaveChangesAsync();
                return (returnUrl == null) ? RedirectToAction("Index") : RedirectToAction("/Index", "Foro");
            }

            return View(temas);
        }

        // GET: Temas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = await db.Temas.FindAsync(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            return View(temas);
        }

        // POST: Temas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Descripcion,Activo")] Temas temas)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            temas.User = user;
            temas.Fecha = DateTime.Now;
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                db.Entry(temas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(temas);
        }

        // GET: Temas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = await db.Temas.FindAsync(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            return View(temas);
        }

        // POST: Temas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Temas temas = await db.Temas.FindAsync(id);
            db.Temas.Remove(temas);
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
