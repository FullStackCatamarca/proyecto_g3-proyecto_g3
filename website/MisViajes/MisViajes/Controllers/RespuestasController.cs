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
    public class RespuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Respuestas
        public async Task<ActionResult> Index()
        {
            return View(await db.Respuestas.ToListAsync());
        }

        // GET: Respuestas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuestas respuestas = await db.Respuestas.FindAsync(id);
            if (respuestas == null)
            {
                return HttpNotFound();
            }
            return View(respuestas);
        }

        // GET: Respuestas/Create
        public ActionResult Create(int? id = null)
        {
            if (id == null) return RedirectToAction("Index", "Foro");

            Respuestas respuesta = new Respuestas();
            var post = db.Posts.Where(r => r.Id == id);
            ViewBag.postId = new SelectList(post, "Id", "Descripcion");
 
            return View();
        }

        // POST: Respuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Descripcion,Post")] Respuestas respuestas, FormCollection form)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            respuestas.User = user;
            respuestas.Fecha = DateTime.Now;
            int id = Convert.ToInt32(form["postId"]);
            respuestas.Post = db.Posts.Where(r => r.Id == id).FirstOrDefault();

            ModelState.Remove("Fecha");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                db.Respuestas.Add(respuestas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Foro");
            }

            return View(respuestas);
        }

        // GET: Respuestas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuestas respuestas = await db.Respuestas.FindAsync(id);
            if (respuestas == null)
            {
                return HttpNotFound();
            }
            return View(respuestas);
        }

        // POST: Respuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Fecha,Descripcion")] Respuestas respuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuestas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(respuestas);
        }

        // GET: Respuestas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuestas respuestas = await db.Respuestas.FindAsync(id);
            if (respuestas == null)
            {
                return HttpNotFound();
            }
            return View(respuestas);
        }

        // POST: Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Respuestas respuestas = await db.Respuestas.FindAsync(id);
            db.Respuestas.Remove(respuestas);
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
