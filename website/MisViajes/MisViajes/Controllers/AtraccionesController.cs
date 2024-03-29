﻿using System;
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

    public class AtraccionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Atracciones
 
        public async Task<ActionResult> Index(string order = "0")
        {
            if (User.IsInRole("Staff") || User.IsInRole("Administrador"))
            {
                ViewBag.Message = "Confirmado";


            }
            List<Atracciones> atracciones = new List<Atracciones>();
            var servicios = await db.Servicios.ToListAsync();
            foreach (var s in servicios)
            {
                var a = s.GetType().ToString();
                if (s.GetType().ToString() == "MisViajes.Models.Atracciones")
                {
                    atracciones.Add((Atracciones)s);
                }
            }
            var masPopulares = atracciones.OrderByDescending(x => float.Parse(x.Puntuacion));
           
            if (order == "0")
            {
                return View(atracciones);
            }
            if (order == "1")
            {
                return View(masPopulares);
            }
            return View(atracciones);
        }

        // GET: Atracciones/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atracciones atracciones = (Atracciones) await db.Servicios.FindAsync(id);
            if (atracciones == null)
            {
                return HttpNotFound();
            }
            return View(atracciones);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> coordenadas(int? idServ)
        {
            Atracciones atracciones = (Atracciones)await db.Servicios.FindAsync(idServ);
            return Json(atracciones, JsonRequestBehavior.AllowGet);
        }


        // GET: Atracciones/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Atracciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud")] Atracciones atracciones)
        {
            if (ModelState.IsValid)
            {
                db.Servicios.Add(atracciones);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(atracciones);
        }

        // GET: Atracciones/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atracciones atracciones = (Atracciones) await db.Servicios.FindAsync(id);
            if (atracciones == null)
            {
                return HttpNotFound();
            }
            return View(atracciones);
        }

        // POST: Atracciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Descripcion,Ubicacion,Fotourl,costo,Puntuacion,Localidad,weburl,habilitado,Latitud,Longitud")] Atracciones atracciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atracciones).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(atracciones);
        }

        // GET: Atracciones/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atracciones atracciones = (Atracciones) await db.Servicios.FindAsync(id);
            if (atracciones == null)
            {
                return HttpNotFound();
            }
            return View(atracciones);
        }

        // POST: Atracciones/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Atracciones atracciones = (Atracciones) await db.Servicios.FindAsync(id);
            db.Servicios.Remove(atracciones);
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
