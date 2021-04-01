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
    public class RespuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Respuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Fecha,Descripcion")] Respuestas respuestas)
        {
            if (ModelState.IsValid)
            {
                db.Respuestas.Add(respuestas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
