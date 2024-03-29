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
    public class ProvinciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Provincias
        public async Task<ActionResult> Index()
        {
            var provincias = db.Provincias.Include(p => p.Pais);
            return View(await provincias.ToListAsync());
        }

        // GET: Provincias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = await db.Provincias.FindAsync(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            return View(provincias);
        }

        // GET: Provincias/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre");
            return View();
        }

        // POST: Provincias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,PaisId")] Provincias provincias)
        {
            if (ModelState.IsValid)
            {
                db.Provincias.Add(provincias);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre", provincias.PaisId);
            return View(provincias);
        }

        // GET: Provincias/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = await db.Provincias.FindAsync(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre", provincias.PaisId);
            return View(provincias);
        }

        // POST: Provincias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,PaisId")] Provincias provincias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provincias).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre", provincias.PaisId);
            return View(provincias);
        }

        // GET: Provincias/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = await db.Provincias.FindAsync(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            return View(provincias);
        }

        // POST: Provincias/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Provincias provincias = await db.Provincias.FindAsync(id);
            db.Provincias.Remove(provincias);
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
