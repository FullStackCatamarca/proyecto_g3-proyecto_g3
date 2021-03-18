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


namespace MisViajes.Controllers
{
    public class WaypointsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Waypoints
        public async Task<ActionResult> Index()
        {
            var waypoints = db.Waypoints.Include(w => w.ruta).Include(w => w.Servicios);
            return View(await waypoints.ToListAsync());
        }

        // GET: Waypoints/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waypoint waypoint = await db.Waypoints.FindAsync(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        // GET: Waypoints/Create
        public ActionResult Create()

        {   var id = User.Identity.GetUserId().ToString();
            var ruta = db.Rutas.Where(r => r.User.Id == id).Where(a => a.Abierto == true);
 
            ViewBag.rutaId = new SelectList(ruta, "Id", "Nombre");
            ViewBag.ServiciosId = new SelectList(db.Servicios.Where(s => s.habilitado == true), "Id", "Nombre");
            return View();
        }

        // POST: Waypoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,rutaId,ServiciosId")] Waypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                db.Waypoints.Add(waypoint);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var id = User.Identity.GetUserId().ToString();
            var ruta = db.Rutas.Where(r => r.User.Id == id).Where(a => a.Abierto == true);

            ViewBag.rutaId = new SelectList(ruta, "Id", "Nombre", waypoint.rutaId);
            ViewBag.ServiciosId = new SelectList(db.Servicios.Where(s => s.habilitado == true), "Id", "Nombre", waypoint.ServiciosId);

            return View(waypoint);
        }

        // GET: Waypoints/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waypoint waypoint = await db.Waypoints.FindAsync(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.rutaId = new SelectList(db.Rutas, "Id", "Nombre", waypoint.rutaId);
            ViewBag.ServiciosId = new SelectList(db.Servicios, "Id", "Nombre", waypoint.ServiciosId);
            return View(waypoint);
        }

        // POST: Waypoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,rutaId,ServiciosId")] Waypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waypoint).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.rutaId = new SelectList(db.Rutas, "Id", "Nombre", waypoint.rutaId);
            ViewBag.ServiciosId = new SelectList(db.Servicios, "Id", "Nombre", waypoint.ServiciosId);
            return View(waypoint);
        }

        // GET: Waypoints/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waypoint waypoint = await db.Waypoints.FindAsync(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        // POST: Waypoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Waypoint waypoint = await db.Waypoints.FindAsync(id);
            db.Waypoints.Remove(waypoint);
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
