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
    public class BrugerLogInsController : ApiController
    {
        private FlygoContext db = new FlygoContext();

        // GET: api/BrugerLogIns
        public IQueryable<BrugerLogIn> GetBrugerLogIn()
        {
            return db.BrugerLogIn;
        }

        // GET: api/BrugerLogIns/5
        [ResponseType(typeof(BrugerLogIn))]
        public IHttpActionResult GetBrugerLogIn(int id)
        {
            BrugerLogIn brugerLogIn = db.BrugerLogIn.Find(id);
            if (brugerLogIn == null)
            {
                return NotFound();
            }

            return Ok(brugerLogIn);
        }

        // PUT: api/BrugerLogIns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrugerLogIn(int id, BrugerLogIn brugerLogIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brugerLogIn.Id)
            {
                return BadRequest();
            }

            db.Entry(brugerLogIn).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrugerLogInExists(id))
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

        // POST: api/BrugerLogIns
        [ResponseType(typeof(BrugerLogIn))]
        public IHttpActionResult PostBrugerLogIn(BrugerLogIn brugerLogIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BrugerLogIn.Add(brugerLogIn);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brugerLogIn.Id }, brugerLogIn);
        }

        // DELETE: api/BrugerLogIns/5
        [ResponseType(typeof(BrugerLogIn))]
        public IHttpActionResult DeleteBrugerLogIn(int id)
        {
            BrugerLogIn brugerLogIn = db.BrugerLogIn.Find(id);
            if (brugerLogIn == null)
            {
                return NotFound();
            }

            db.BrugerLogIn.Remove(brugerLogIn);
            db.SaveChanges();

            return Ok(brugerLogIn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BrugerLogInExists(int id)
        {
            return db.BrugerLogIn.Count(e => e.Id == id) > 0;
        }
    }
}