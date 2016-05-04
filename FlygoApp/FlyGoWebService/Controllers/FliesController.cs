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
using FlyGoWebService.Models;

namespace FlyGoWebService.Controllers
{
    public class FliesController : ApiController
    {
        private FlygoContext db = new FlygoContext();

        // GET: api/Flies
        public IQueryable<Fly> GetFly()
        {
            return db.Fly;
        }

        // GET: api/Flies/5
        [ResponseType(typeof(Fly))]
        public IHttpActionResult GetFly(int id)
        {
            Fly fly = db.Fly.Find(id);
            if (fly == null)
            {
                return NotFound();
            }

            return Ok(fly);
        }

        // PUT: api/Flies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFly(int id, Fly fly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fly.Id)
            {
                return BadRequest();
            }

            db.Entry(fly).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlyExists(id))
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

        // POST: api/Flies
        [ResponseType(typeof(Fly))]
        public IHttpActionResult PostFly(Fly fly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fly.Add(fly);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fly.Id }, fly);
        }

        // DELETE: api/Flies/5
        [ResponseType(typeof(Fly))]
        public IHttpActionResult DeleteFly(int id)
        {
            Fly fly = db.Fly.Find(id);
            if (fly == null)
            {
                return NotFound();
            }

            db.Fly.Remove(fly);
            db.SaveChanges();

            return Ok(fly);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlyExists(int id)
        {
            return db.Fly.Count(e => e.Id == id) > 0;
        }
    }
}