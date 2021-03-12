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
    public class UsuarioModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsuarioModels
        public async Task<ActionResult> Index()
        {
           List <UsuarioModel> listUsuarios = new MantenimientoUsuario().RecuperarTodos();
            return View(listUsuarios);
        }

        // GET: UsuarioModels/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = await db.UsuarioModels.FindAsync(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }

        // GET: UsuarioModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Apellido,Dni,Domicilio,Pais,Provincia,Departamento,FechaNacimiento,Sexo,AspNetUser,AvatarUrl,Posicion,Descripcion,CodigoPostal,Acerca,ImgUrl")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                db.UsuarioModels.Add(usuarioModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(usuarioModel);
        }

        // GET: UsuarioModels/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = await db.UsuarioModels.FindAsync(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }

        // POST: UsuarioModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Apellido,Dni,Domicilio,Pais,Provincia,Departamento,FechaNacimiento,Sexo,AspNetUser,AvatarUrl,Posicion,Descripcion,CodigoPostal,Acerca,ImgUrl")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(usuarioModel);
        }

        // GET: UsuarioModels/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel usuarioModel = await db.UsuarioModels.FindAsync(id);
            if (usuarioModel == null)
            {
                return HttpNotFound();
            }
            return View(usuarioModel);
        }

        // POST: UsuarioModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            UsuarioModel usuarioModel = await db.UsuarioModels.FindAsync(id);
            db.UsuarioModels.Remove(usuarioModel);
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
