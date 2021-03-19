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

namespace MisViajes.Controllers
{
    public class SlidesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Slides
        public async Task<ActionResult> Index()
        {
            return View(await db.Slides.ToListAsync());
        }

        // GET: Slides/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slides slides = await db.Slides.FindAsync(id);
            if (slides == null)
            {
                return HttpNotFound();
            }
            return View(slides);
        }

        // GET: Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,UrlImagen,Encabezado,Titulo,Descripcion,UrlDestino,TxtBoton,Habilitado")] Slides slides)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Add(slides);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(slides);
        }

        // GET: Slides/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slides slides = await db.Slides.FindAsync(id);
            if (slides == null)
            {
                return HttpNotFound();
            }
            return View(slides);
        }

        // POST: Slides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,UrlImagen,Encabezado,Titulo,Descripcion,UrlDestino,TxtBoton,Habilitado")] Slides slides)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slides).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slides);
        }

        // GET: Slides/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slides slides = await db.Slides.FindAsync(id);
            if (slides == null)
            {
                return HttpNotFound();
            }
            return View(slides);
        }

        // POST: Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Slides slides = await db.Slides.FindAsync(id);
            db.Slides.Remove(slides);
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
