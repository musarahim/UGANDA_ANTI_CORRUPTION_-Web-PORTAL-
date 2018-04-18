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
    public class ActivitiesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ActivitiesAPI
        public IQueryable<Activity> Getactivities()
        {
            return db.activities;
        }

        // GET: api/ActivitiesAPI/5
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> GetActivity(int id)
        {
            Activity activity = await db.activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/ActivitiesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivity(int id, Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activity.ActivityID)
            {
                return BadRequest();
            }

            db.Entry(activity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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

        // POST: api/ActivitiesAPI
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> PostActivity(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.activities.Add(activity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activity.ActivityID }, activity);
        }

        // DELETE: api/ActivitiesAPI/5
        [ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> DeleteActivity(int id)
        {
            Activity activity = await db.activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            db.activities.Remove(activity);
            await db.SaveChangesAsync();

            return Ok(activity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityExists(int id)
        {
            return db.activities.Count(e => e.ActivityID == id) > 0;
        }
    }
}