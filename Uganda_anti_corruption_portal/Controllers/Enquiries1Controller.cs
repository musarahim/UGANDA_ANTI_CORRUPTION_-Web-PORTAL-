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
    public class Enquiries1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Enquiries1
        public IQueryable<Enquiry> GetEnquiries()
        {
            return db.Enquiries;
        }

        // GET: api/Enquiries1/5
        [ResponseType(typeof(Enquiry))]
        public async Task<IHttpActionResult> GetEnquiry(int id)
        {
            Enquiry enquiry = await db.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return NotFound();
            }

            return Ok(enquiry);
        }

        // PUT: api/Enquiries1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEnquiry(int id, Enquiry enquiry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enquiry.EnquiryID)
            {
                return BadRequest();
            }

            db.Entry(enquiry).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnquiryExists(id))
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

        // POST: api/Enquiries1
        [ResponseType(typeof(Enquiry))]
        public async Task<IHttpActionResult> PostEnquiry(Enquiry enquiry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enquiries.Add(enquiry);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = enquiry.EnquiryID }, enquiry);
        }

        // DELETE: api/Enquiries1/5
        [ResponseType(typeof(Enquiry))]
        public async Task<IHttpActionResult> DeleteEnquiry(int id)
        {
            Enquiry enquiry = await db.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return NotFound();
            }

            db.Enquiries.Remove(enquiry);
            await db.SaveChangesAsync();

            return Ok(enquiry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnquiryExists(int id)
        {
            return db.Enquiries.Count(e => e.EnquiryID == id) > 0;
        }
    }
}