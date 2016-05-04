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
    public class FlyRutesController : ApiController
    {
        private FlygoContext db = new FlygoContext();

        // GET: api/FlyRutes
        public IQueryable<FlyRute> GetFlyRute()
        {
            return db.FlyRute;
        }

        // GET: api/FlyRutes/5
        [ResponseType(typeof(FlyRute))]
        public IHttpActionResult GetFlyRute(int id)
        {
            FlyRute flyRute = db.FlyRute.Find(id);
            if (flyRute == null)
            {
                return NotFound();
            }

            return Ok(flyRute);
        }

        // PUT: api/FlyRutes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlyRute(int id, FlyRute flyRute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flyRute.Id)
            {
                return BadRequest();
            }

            db.Entry(flyRute).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlyRuteExists(id))
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

        // POST: api/FlyRutes
        [ResponseType(typeof(FlyRute))]
        public IHttpActionResult PostFlyRute(FlyRute flyRute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FlyRute.Add(flyRute);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flyRute.Id }, flyRute);
        }

        // DELETE: api/FlyRutes/5
        [ResponseType(typeof(FlyRute))]
        public IHttpActionResult DeleteFlyRute(int id)
        {
            FlyRute flyRute = db.FlyRute.Find(id);
            if (flyRute == null)
            {
                return NotFound();
            }

            db.FlyRute.Remove(flyRute);
            db.SaveChanges();

            return Ok(flyRute);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlyRuteExists(int id)
        {
            return db.FlyRute.Count(e => e.Id == id) > 0;
        }
    }
}