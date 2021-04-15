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
    public class MonumentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Monumentos
        public async Task<ActionResult> Index(string order="0")
        {
            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";


            }
            List<Monumentos>monumentos  = new List<Monumentos>();
            var servicios = await db.Servicios.ToListAsync();
            foreach (var s in servicios)
            {
                var a = s.GetType().ToString();
                if (s.GetType().ToString() == "MisViajes.Models.Monumentos")
                {
                    monumentos.Add((Monumentos)s);
                }
            }

            var masPopulares = monumentos.OrderByDescending(x => float.Parse(x.Puntuacion));
            var masEconomicos = monumentos.OrderBy(x => x.costo);
          

            if (order == "0")
            {
                return View(monumentos);
            }
            if (order == "1")
            {
                return View(masPopulares);
            }
            if (order == "2")
            {
                return View(masEconomicos);
            }

            return View(monumentos);
        }

        // GET: Monumentos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monumentos monumentos = (Monumentos) await db.Servicios.FindAsync(id);
            if (monumentos == null)
            {
                return HttpNotFound();
            }
            return View(monumentos);
        }

        [HttpPost]
        public async Task<JsonResult> coordenadas(int? idServ)
        {
            Monumentos monumentos = (Monumentos)await db.Servicios.FindAsync(idServ);
            return Json(monumentos, JsonRequestBehavior.AllowGet);
        }

        // GET: Monumentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Monumentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud")] Monumentos monumentos)
        {
            if (ModelState.IsValid)
            {
                db.Servicios.Add(monumentos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(monumentos);
        }

        // GET: Monumentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monumentos monumentos = (Monumentos) await db.Servicios.FindAsync(id);
            if (monumentos == null)
            {
                return HttpNotFound();
            }
            return View(monumentos);
        }

        // POST: Monumentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud")] Monumentos monumentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monumentos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(monumentos);
        }

        // GET: Monumentos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monumentos monumentos = (Monumentos) await db.Servicios.FindAsync(id);
            if (monumentos == null)
            {
                return HttpNotFound();
            }
            return View(monumentos);
        }

        // POST: Monumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Monumentos monumentos = (Monumentos) await db.Servicios.FindAsync(id);
            db.Servicios.Remove(monumentos);
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
