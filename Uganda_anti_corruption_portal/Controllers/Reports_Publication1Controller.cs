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
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class Reports_Publication1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Reports_Publication1
        public IQueryable<Reports_Publication> GetReports_Publication()
        {
            return db.Reports_Publication;
        }

        // GET: api/Reports_Publication1/5
        [ResponseType(typeof(Reports_Publication))]
        public async Task<IHttpActionResult> GetReports_Publication(int id)
        {
            Reports_Publication reports_Publication = await db.Reports_Publication.FindAsync(id);
            if (reports_Publication == null)
            {
                return NotFound();
            }

            return Ok(reports_Publication);
        }

        // PUT: api/Reports_Publication1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReports_Publication(int id, Reports_Publication reports_Publication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reports_Publication.Reports_PublicationID)
            {
                return BadRequest();
            }

            db.Entry(reports_Publication).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Reports_PublicationExists(id))
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

        // POST: api/Reports_Publication1
        [ResponseType(typeof(Reports_Publication))]
        public async Task<IHttpActionResult> PostReports_Publication(Reports_Publication reports_Publication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reports_Publication.Add(reports_Publication);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reports_Publication.Reports_PublicationID }, reports_Publication);
        }

        // DELETE: api/Reports_Publication1/5
        [ResponseType(typeof(Reports_Publication))]
        public async Task<IHttpActionResult> DeleteReports_Publication(int id)
        {
            Reports_Publication reports_Publication = await db.Reports_Publication.FindAsync(id);
            if (reports_Publication == null)
            {
                return NotFound();
            }

            db.Reports_Publication.Remove(reports_Publication);
            await db.SaveChangesAsync();

            return Ok(reports_Publication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Reports_PublicationExists(int id)
        {
            return db.Reports_Publication.Count(e => e.Reports_PublicationID == id) > 0;
        }
    }
}