using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MisViajes.Models
{
    public class LocalidadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Localidades
        public async Task<ActionResult> Index()
        {
            var localidades = db.Localidades.Include(l => l.Departamento);
            return View(await localidades.ToListAsync());
        }

        // GET: Localidades/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidades localidades = await db.Localidades.FindAsync(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            return View(localidades);
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentoId = new SelectList(db.Departamentos, "Id", "Nombre");
            return View();
        }

        // POST: Localidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Latitud,Longitud,DepartamentoId")] Localidades localidades)
        {
            if (ModelState.IsValid)
            {
                db.Localidades.Add(localidades);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoId = new SelectList(db.Departamentos, "Id", "Nombre", localidades.DepartamentoId);
            return View(localidades);
        }

        // GET: Localidades/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidades localidades = await db.Localidades.FindAsync(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoId = new SelectList(db.Departamentos, "Id", "Nombre", localidades.DepartamentoId);
            return View(localidades);
        }

        // POST: Localidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Latitud,Longitud,DepartamentoId")] Localidades localidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localidades).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoId = new SelectList(db.Departamentos, "Id", "Nombre", localidades.DepartamentoId);
            return View(localidades);
        }

        // GET: Localidades/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidades localidades = await db.Localidades.FindAsync(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            return View(localidades);
        }

        // POST: Localidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Localidades localidades = await db.Localidades.FindAsync(id);
            db.Localidades.Remove(localidades);
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
