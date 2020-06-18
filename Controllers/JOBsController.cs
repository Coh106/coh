using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPIWebApplication.Models;

namespace WebAPIWebApplication.Controllers
{
    public class JOBsController : ApiController
    {
        private ROUTEEntities db = new ROUTEEntities();

        // GET: api/JOBs
        public IQueryable<JOB> GetJOBS()
        {
            return db.JOBS;
        }

        // GET: api/JOBs/5
        [ResponseType(typeof(JOB))]
        public IHttpActionResult GetJOB(int id)
        {
            JOB jOB = db.JOBS.Find(id);
            if (jOB == null)
            {
                return NotFound();
            }

            return Ok(jOB);
        }

        // PUT: api/JOBs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJOB(int id, JOB jOB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jOB.ID)
            {
                return BadRequest();
            }

            db.Entry(jOB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JOBExists(id))
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

        // POST: api/JOBs
        [ResponseType(typeof(JOB))]
        public IHttpActionResult PostJOB(JOB jOB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JOBS.Add(jOB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jOB.ID }, jOB);
        }

        // DELETE: api/JOBs/5
        [ResponseType(typeof(JOB))]
        public IHttpActionResult DeleteJOB(int id)
        {
            JOB jOB = db.JOBS.Find(id);
            if (jOB == null)
            {
                return NotFound();
            }

            db.JOBS.Remove(jOB);
            db.SaveChanges();

            return Ok(jOB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JOBExists(int id)
        {
            return db.JOBS.Count(e => e.ID == id) > 0;
        }
    }
}