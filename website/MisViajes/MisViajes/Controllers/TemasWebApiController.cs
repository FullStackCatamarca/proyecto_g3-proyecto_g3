using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MisViajes.Models;
using static MisViajes.Models.ApiTemas;

namespace MisViajes.Controllers
{
    public class TemasWebApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TemasWebApi
        public IQueryable<ApiTemas> GetTemas(string order="0")
        {
            List <ApiTemas> ATemas = new List<ApiTemas>();
            List<ApiTemas> OATemas = new List<ApiTemas>();
            IQueryable temas = db.Temas.Include(m => m.User);
            foreach (Temas t in temas)
            {
                var u = (ApiTemas) t;
                u.Posts = CargarPosts(t.Id);
                u.Respuestas = u.Posts.Length;
                ATemas.Add(u);
            }

            //ultimo primero
            if (order == "0")
            {
                var list = from a in ATemas
                           orderby a.Fecha
                           select a;
                Array.ForEach<ApiTemas>(list.Reverse().ToArray<ApiTemas>(), a => OATemas.Add(a));
            }
            //Mayor Cantidad de Post
            if (order == "1")
            {
                var list = from a in ATemas
                           orderby a.Respuestas
                           select a;
                Array.ForEach<ApiTemas>(list.ToArray<ApiTemas>(), a => OATemas.Add(a));
            }

            //Menor Cantidad de Post
            if (order == "2")
            {
                var list = from a in ATemas
                           orderby a.Respuestas
                           select a;
                Array.ForEach<ApiTemas>(list.Reverse().ToArray<ApiTemas>(), a => OATemas.Add(a));
            }

            return OATemas.AsQueryable();
        }

        private ApiPosts[] CargarPosts(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<ApiPosts> LAPosts = new List<ApiPosts>();

            ApiPosts[] APosts = new ApiPosts[] { };
            var posts = db.Posts.Include(p => p.User).Include(p=> p.Tema).Where(p => p.Tema.Id == id).ToList();
            foreach(Posts p in posts)
            {
                List<ApiRespuestas> LARespuestas = new List<ApiRespuestas>();

                ApiRespuestas[] respuesta = new ApiRespuestas[] { };
                var respuestas = db.Respuestas.Include(r => r.User).Include(r => r.Post).Where(r => r.Post.Id == p.Id).ToList();
                ApiPosts ap = (ApiPosts)p;
                
                foreach (Respuestas r in respuestas)
                {
                    LARespuestas.Add((ApiRespuestas) r);
                }
                ap.Respuesta = LARespuestas.ToArray();
                ap.Respuestas = ap.Respuesta.Length;

                var votos = db.Votos.Include(v => v.Post).Where(v => v.Post.Id == p.Id);

                ap.VotosDown = votos.Count(v => v.Up == false);
                ap.VotosUp = votos.Count(v => v.Up == true);


                LAPosts.Add(ap);
            }
            APosts = LAPosts.ToArray();

            return APosts;
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