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
    [Authorize]
    public class EventosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Eventos
        public async Task<ActionResult> Index(string order="0")
        {
            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";

            }
            List<Eventos>eventos=new List<Eventos>();
            var servicios = await db.Servicios.ToListAsync();
            foreach (var s in servicios)
            {
                var a = s.GetType().ToString();
                if (s.GetType().ToString() == "MisViajes.Models.Eventos")
                {
                    eventos.Add((Eventos)s);
                }
            }
            var masPopulares = eventos.OrderByDescending(x => float.Parse(x.Puntuacion));
            var masEconomicos = eventos.OrderBy(x => x.costo);


            if (order == "0")
            {
                return View(eventos);
            }
            if (order == "1")
            {
                return View(masPopulares);
            }
            if (order == "2")
            {
                return View(masEconomicos);
            }
            return View(eventos);
        }

        // GET: Eventos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventos eventos = (Eventos) await db.Servicios.FindAsync(id);
            if (eventos == null)
            {
                return HttpNotFound();
            }
            return View(eventos);
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud,Inicio,Fin")] Eventos eventos)
        {
            if (ModelState.IsValid)
            {
                db.Servicios.Add(eventos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eventos);
        }

        // GET: Eventos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventos eventos = (Eventos) await db.Servicios.FindAsync(id);
            if (eventos == null)
            {
                return HttpNotFound();
            }
            return View(eventos);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud,Inicio,Fin")] Eventos eventos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eventos);
        }

        // GET: Eventos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventos eventos = (Eventos) await db.Servicios.FindAsync(id);
            if (eventos == null)
            {
                return HttpNotFound();
            }
            return View(eventos);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Eventos eventos = (Eventos) await db.Servicios.FindAsync(id);
            db.Servicios.Remove(eventos);
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
