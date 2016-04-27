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
using FlyGoWebService;

namespace FlyGoWebService.Controllers
{
    public class HangarsController : ApiController
    {
        private FlyGoEF db = new FlyGoEF();

        // GET: api/Hangars
        public IQueryable<Hangar> GetHangar()
        {
            return db.Hangar;
        }

        // GET: api/Hangars/5
        [ResponseType(typeof(Hangar))]
        public IHttpActionResult GetHangar(int id)
        {
            Hangar hangar = db.Hangar.Find(id);
            if (hangar == null)
            {
                return NotFound();
            }

            return Ok(hangar);
        }

        // PUT: api/Hangars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHangar(int id, Hangar hangar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hangar.Id)
            {
                return BadRequest();
            }

            db.Entry(hangar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HangarExists(id))
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

        // POST: api/Hangars
        [ResponseType(typeof(Hangar))]
        public IHttpActionResult PostHangar(Hangar hangar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hangar.Add(hangar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hangar.Id }, hangar);
        }

        // DELETE: api/Hangars/5
        [ResponseType(typeof(Hangar))]
        public IHttpActionResult DeleteHangar(int id)
        {
            Hangar hangar = db.Hangar.Find(id);
            if (hangar == null)
            {
                return NotFound();
            }

            db.Hangar.Remove(hangar);
            db.SaveChanges();

            return Ok(hangar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HangarExists(int id)
        {
            return db.Hangar.Count(e => e.Id == id) > 0;
        }
    }
}