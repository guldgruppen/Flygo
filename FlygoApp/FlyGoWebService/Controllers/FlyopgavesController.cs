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
    public class FlyopgavesController : ApiController
    {
        private FlygoEntityFrameworkContext db = new FlygoEntityFrameworkContext();

        // GET: api/Flyopgaves
        public IQueryable<Flyopgave> GetFlyopgave()
        {
            return db.Flyopgave;
        }
        [HttpGet]
        // GET: api/Flyopgaves/5
        [ResponseType(typeof(Flyopgave))]
        public IHttpActionResult GetFlyopgave(int id)
        {
            Flyopgave flyopgave = db.Flyopgave.Find(id);
            if (flyopgave == null)
            {
                return NotFound();
            }

            return Ok(flyopgave);
        }

        // PUT: api/Flyopgaves/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlyopgave(int id, Flyopgave flyopgave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flyopgave.Id)
            {
                return BadRequest();
            }

            db.Entry(flyopgave).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlyopgaveExists(id))
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

        // POST: api/Flyopgaves
        [ResponseType(typeof(Flyopgave))]
        public IHttpActionResult PostFlyopgave(Flyopgave flyopgave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flyopgave.Add(flyopgave);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flyopgave.Id }, flyopgave);
        }

        // DELETE: api/Flyopgaves/5
        [ResponseType(typeof(Flyopgave))]
        public IHttpActionResult DeleteFlyopgave(int id)
        {
            Flyopgave flyopgave = db.Flyopgave.Find(id);
            if (flyopgave == null)
            {
                return NotFound();
            }

            db.Flyopgave.Remove(flyopgave);
            db.SaveChanges();

            return Ok(flyopgave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlyopgaveExists(int id)
        {
            return db.Flyopgave.Count(e => e.Id == id) > 0;
        }
    }
}