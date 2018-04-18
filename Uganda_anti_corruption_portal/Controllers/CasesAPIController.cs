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
    public class CasesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CasesAPI
        public IQueryable<Case> GetCases()
        {
            return db.Cases;
        }

        // GET: api/CasesAPI/5
        [ResponseType(typeof(Case))]
        public async Task<IHttpActionResult> GetCase(int id)
        {
            Case @case = await db.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }

            return Ok(@case);
        }

        // PUT: api/CasesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCase(int id, Case @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @case.CaseID)
            {
                return BadRequest();
            }

            db.Entry(@case).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(id))
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

        // POST: api/CasesAPI
        [ResponseType(typeof(Case))]
        public async Task<IHttpActionResult> PostCase(Case @case)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cases.Add(@case);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @case.CaseID }, @case);
        }

        // DELETE: api/CasesAPI/5
        [ResponseType(typeof(Case))]
        public async Task<IHttpActionResult> DeleteCase(int id)
        {
            Case @case = await db.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }

            db.Cases.Remove(@case);
            await db.SaveChangesAsync();

            return Ok(@case);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaseExists(int id)
        {
            return db.Cases.Count(e => e.CaseID == id) > 0;
        }
    }
}