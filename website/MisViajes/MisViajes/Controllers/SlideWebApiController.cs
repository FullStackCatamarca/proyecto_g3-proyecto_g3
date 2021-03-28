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
    public class SlideWebApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SlideWebApi
        public IQueryable<Slides> GetSlides()
        {
            return db.Slides;
        }

        // GET: api/SlideWebApi/5
        [ResponseType(typeof(Slides))]
        public async Task<IHttpActionResult> GetSlides(int id)
        {
            Slides slides = await db.Slides.FindAsync(id);
            if (slides == null)
            {
                return NotFound();
            }

            return Ok(slides);
        }

        // PUT: api/SlideWebApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSlides(int id, Slides slides)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != slides.id)
            {
                return BadRequest();
            }

            db.Entry(slides).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlidesExists(id))
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

        // POST: api/SlideWebApi
        [ResponseType(typeof(Slides))]
        public async Task<IHttpActionResult> PostSlides(Slides slides)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Slides.Add(slides);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = slides.id }, slides);
        }

        // DELETE: api/SlideWebApi/5
        [ResponseType(typeof(Slides))]
        public async Task<IHttpActionResult> DeleteSlides(int id)
        {
            Slides slides = await db.Slides.FindAsync(id);
            if (slides == null)
            {
                return NotFound();
            }

            db.Slides.Remove(slides);
            await db.SaveChangesAsync();

            return Ok(slides);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SlidesExists(int id)
        {
            return db.Slides.Count(e => e.id == id) > 0;
        }
    }
}