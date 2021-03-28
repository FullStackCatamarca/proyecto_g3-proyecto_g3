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
    public class VotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Votos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Fecha,Up")] Votos votos)
        {
            if (ModelState.IsValid)
            {
                db.Votos.Add(votos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(votos);
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
