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
    public class OpgaveArkivsController : ApiController
    {
        private FlyGoEF db = new FlyGoEF();

        // GET: api/OpgaveArkivs
        public IQueryable<OpgaveArkiv> GetOpgaveArkiv()
        {
            return db.OpgaveArkiv;
        }

        // GET: api/OpgaveArkivs/5
        [ResponseType(typeof(OpgaveArkiv))]
        public IHttpActionResult GetOpgaveArkiv(int id)
        {
            OpgaveArkiv opgaveArkiv = db.OpgaveArkiv.Find(id);
            if (opgaveArkiv == null)
            {
                return NotFound();
            }

            return Ok(opgaveArkiv);
        }

        // PUT: api/OpgaveArkivs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOpgaveArkiv(int id, OpgaveArkiv opgaveArkiv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != opgaveArkiv.Id)
            {
                return BadRequest();
            }

            db.Entry(opgaveArkiv).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpgaveArkivExists(id))
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

        // POST: api/OpgaveArkivs
        [ResponseType(typeof(OpgaveArkiv))]
        public IHttpActionResult PostOpgaveArkiv(OpgaveArkiv opgaveArkiv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OpgaveArkiv.Add(opgaveArkiv);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = opgaveArkiv.Id }, opgaveArkiv);
        }

        // DELETE: api/OpgaveArkivs/5
        [ResponseType(typeof(OpgaveArkiv))]
        public IHttpActionResult DeleteOpgaveArkiv(int id)
        {
            OpgaveArkiv opgaveArkiv = db.OpgaveArkiv.Find(id);
            if (opgaveArkiv == null)
            {
                return NotFound();
            }

            db.OpgaveArkiv.Remove(opgaveArkiv);
            db.SaveChanges();

            return Ok(opgaveArkiv);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OpgaveArkivExists(int id)
        {
            return db.OpgaveArkiv.Count(e => e.Id == id) > 0;
        }
    }
}