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
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
           List <UsuarioModel> list_users = new MantenimientoUsuario().RecuperarTodos();

            return View(list_users);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser Users = new MantenimientoUsuario().RecuperarUsuario(id);
            if (Users == null)
            {
                return HttpNotFound();
            }
            return View(Users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
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

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser model = new MantenimientoUsuario().RecuperarUsuario(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Apellido,Dni,PhoneNumber,UserName,Domicilio,Pais,Provincia,Departamento,FechaNacimiento,Sexo,AvatarUrl,Posicion,Descripcion,CodigoPostal,Acerca")] UsuarioModel usuarioModel)
        {
            usuarioModel.Nombre = usuarioModel.Nombre;
            usuarioModel.Apellido = usuarioModel.Apellido;
            usuarioModel.Dni = usuarioModel.Dni;
            usuarioModel.PhoneNumber = usuarioModel.PhoneNumber;
            usuarioModel.Domicilio = usuarioModel.Domicilio;
            usuarioModel.Pais = usuarioModel.Pais;
            usuarioModel.Provincia = usuarioModel.Provincia;
            usuarioModel.Departamento = usuarioModel.Departamento;
            usuarioModel.FechaNacimiento = usuarioModel.FechaNacimiento;
            usuarioModel.AvatarUrl = usuarioModel.AvatarUrl;
            usuarioModel.Posicion = usuarioModel.Posicion;
            usuarioModel.Descripcion = usuarioModel.Descripcion;
            usuarioModel.CodigoPostal = usuarioModel.CodigoPostal;
            usuarioModel.Acerca = usuarioModel.Acerca;
            usuarioModel.UserName = usuarioModel.UserName;



            if (ModelState.IsValid)
            {
                int i = new MantenimientoUsuario().ModificarPerfil(usuarioModel);
                return RedirectToAction("Index");
            }
            return View(usuarioModel);
        }

        // GET: Users/Delete/5
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

        // POST: Users/Delete/5
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
