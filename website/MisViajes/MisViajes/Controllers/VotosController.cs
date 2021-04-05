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
    public class VotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Votos
        public async Task<ActionResult> Index()
        {
            return View(await db.Votos.ToListAsync());
        }

        // GET: Votos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Votos votos = await db.Votos.FindAsync(id);
            if (votos == null)
            {
                return HttpNotFound();
            }
            return View(votos);
        }

        // GET: Votos/Create
        public ActionResult Create(int? id = null)
        {
            if (id == null) return RedirectToAction("Index", "Foro");

            var post = db.Posts.Where(p => p.Id == id);
            ViewBag.PostId = new SelectList(post, "Id", "Descripcion");
            return View();
        }

        // POST: Votos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Up,Post")] Votos votos, FormCollection form)
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            votos.User = user;
            votos.Fecha = DateTime.Now;
            int id = Convert.ToInt32(form["postId"]);
            votos.Post = db.Posts.Where(r => r.Id == id).FirstOrDefault();

            ModelState.Remove("Fecha");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                db.Votos.Add(votos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(votos);
        }


        public ActionResult VotoUp(int? postId, bool up)
        {
            if (postId != null)
            {
                Votos votos = new Votos();
                this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
                var UserId = User.Identity.GetUserId();
                var user = UserManager.FindById(UserId);
                votos.User = user;
                votos.Fecha = DateTime.Now;
                votos.Post = db.Posts.Where(r => r.Id == postId).FirstOrDefault();
                votos.Up = up;

                db.Votos.Add(votos);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Foro");
        }

            // GET: Votos/Edit/5
            public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Votos votos = await db.Votos.FindAsync(id);
            if (votos == null)
            {
                return HttpNotFound();
            }
            return View(votos);
        }

        // POST: Votos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Fecha,Up")] Votos votos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(votos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(votos);
        }

        // GET: Votos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Votos votos = await db.Votos.FindAsync(id);
            if (votos == null)
            {
                return HttpNotFound();
            }
            return View(votos);
        }

        // POST: Votos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Votos votos = await db.Votos.FindAsync(id);
            db.Votos.Remove(votos);
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
