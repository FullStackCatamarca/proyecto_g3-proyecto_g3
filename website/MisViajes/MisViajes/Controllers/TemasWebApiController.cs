using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MisViajes.Models;

namespace MisViajes.Controllers
{
    public class TemasWebApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TemasWebApi
        public IQueryable<ApiTemas> GetTemas()
        {
            List <ApiTemas> ATemas = new List<ApiTemas>();
            IQueryable temas = db.Temas.Include(m => m.User);
            foreach (Temas t in temas)
            {
                var u = (ApiTemas) t;
                ATemas.Add(u);
            }

            return ATemas.AsQueryable();
        }

        // GET: api/TemasWebApi/5
        [ResponseType(typeof(Temas))]
        public async Task<IHttpActionResult> GetTemas(int id)
        {
            Temas temas = await db.Temas.FindAsync(id);
            if (temas == null)
            {
                return NotFound();
            }

            return Ok(temas);
        }

        // PUT: api/TemasWebApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTemas(int id, Temas temas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temas.Id)
            {
                return BadRequest();
            }

            db.Entry(temas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TemasWebApi
        [ResponseType(typeof(Temas))]
        public async Task<IHttpActionResult> PostTemas(Temas temas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Temas.Add(temas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = temas.Id }, temas);
        }

        // DELETE: api/TemasWebApi/5
        [ResponseType(typeof(Temas))]
        public async Task<IHttpActionResult> DeleteTemas(int id)
        {
            Temas temas = await db.Temas.FindAsync(id);
            if (temas == null)
            {
                return NotFound();
            }

            db.Temas.Remove(temas);
            await db.SaveChangesAsync();

            return Ok(temas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemasExists(int id)
        {
            return db.Temas.Count(e => e.Id == id) > 0;
        }
    }
}