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
    public class PostsWebApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PostsWebApi
        public IQueryable<Posts> GetPosts()
        {
            return db.Posts;
        }

        // GET: api/PostsWebApi/5
        [ResponseType(typeof(Posts))]
        public async Task<IHttpActionResult> GetPosts(int id)
        {
            Posts posts = await db.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        // PUT: api/PostsWebApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPosts(int id, Posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != posts.Id)
            {
                return BadRequest();
            }

            db.Entry(posts).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(id))
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

        // POST: api/PostsWebApi
        [ResponseType(typeof(Posts))]
        public async Task<IHttpActionResult> PostPosts(Posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Posts.Add(posts);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = posts.Id }, posts);
        }

        // DELETE: api/PostsWebApi/5
        [ResponseType(typeof(Posts))]
        public async Task<IHttpActionResult> DeletePosts(int id)
        {
            Posts posts = await db.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }

            db.Posts.Remove(posts);
            await db.SaveChangesAsync();

            return Ok(posts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostsExists(int id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }
    }
}