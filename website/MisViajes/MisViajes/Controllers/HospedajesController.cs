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
    public class HospedajesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Hospedajes
        public async Task<ActionResult> Index(string order = "0",string localidad = "0")
         {

            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";


            }

            List<Hospedajes> hospedajes= new List<Hospedajes>();
            var servicios = await db.Servicios.ToListAsync();
            foreach(var s in servicios)
            {
                var a = s.GetType().ToString();
                if (s.GetType().ToString() == "MisViajes.Models.Hospedajes")
                {
                    hospedajes.Add((Hospedajes) s);
                }
            }

            var masPopulares = hospedajes.OrderByDescending(x => float.Parse(x.Puntuacion));
            var hospedajeEconomicos = hospedajes.OrderBy(x => x.costo);
            var ord_departamento = hospedajes.OrderBy(x => x.Localidad);

            if (order == "0") {
                return View(hospedajes);
            }
            if(order == "1") {
                return View(masPopulares);
            } 
            if(order == "2") {
                return View(hospedajeEconomicos);
            }




            return View(hospedajes);

        }

        public JsonResult ObtenerDepartamentos()
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<Departamentos> lst = db.Departamentos.ToList<Departamentos>();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ObtenerLocalidades(int? idDepartamento)
        {

            List<Localidades> lst = new List<Localidades>();

            db.Configuration.LazyLoadingEnabled = false;
            if (idDepartamento != null)
                lst = db.Localidades.Where(l => l.DepartamentoId == idDepartamento).ToList<Localidades>();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        // GET: Hospedajes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospedajes hospedajes = (Hospedajes) await db.Servicios.FindAsync(id);
            if (hospedajes == null)
            {
                return HttpNotFound();
            }
           
           
            return View(hospedajes);
        }

        [HttpPost]
        public async Task<JsonResult> coordenadas(int? idServ) {
            Hospedajes hospedajes = (Hospedajes)await db.Servicios.FindAsync(idServ);
            return Json(hospedajes, JsonRequestBehavior.AllowGet);
        }
   
       



        // GET: Hospedajes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hospedajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud")] Hospedajes hospedajes)
        {
            if (ModelState.IsValid)
            {
                db.Servicios.Add(hospedajes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hospedajes);
        }

        // GET: Hospedajes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospedajes hospedajes = (Hospedajes) await db.Servicios.FindAsync(id);
            if (hospedajes == null)
            {
                return HttpNotFound();
            }
            return View(hospedajes);
        }

        // POST: Hospedajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud")] Hospedajes hospedajes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospedajes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hospedajes);
        }

        // GET: Hospedajes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospedajes hospedajes = (Hospedajes) await db.Servicios.FindAsync(id);
            if (hospedajes == null)
            {
                return HttpNotFound();
            }
            return View(hospedajes);
        }

        // POST: Hospedajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Hospedajes hospedajes = (Hospedajes) await db.Servicios.FindAsync(id);
            db.Servicios.Remove(hospedajes);
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
