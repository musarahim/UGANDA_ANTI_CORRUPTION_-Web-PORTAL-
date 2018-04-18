using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class ReportCasesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ReportCasesAPI
        public IQueryable<ReportCase> GetReportCases()
        {
            return db.ReportCases;
        }

        // GET: api/ReportCasesAPI/5
        [ResponseType(typeof(ReportCase))]
        public async Task<IHttpActionResult> GetReportCase(int id)
        {
            ReportCase reportCase = await db.ReportCases.FindAsync(id);
            if (reportCase == null)
            {
                return NotFound();
            }

            return Ok(reportCase);
        }

        // PUT: api/ReportCasesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReportCase(int id, ReportCase reportCase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportCase.ReportCaseID)
            {
                return BadRequest();
            }

            db.Entry(reportCase).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportCaseExists(id))
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

        // POST: api/ReportCasesAPI
        [ResponseType(typeof(ReportCase))]
        public async Task<IHttpActionResult> PostReportCase(ReportCase reportCase)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            //try
            //{
            //    //read the form data
            //    await Request.Content.ReadAsMultipartAsync(provider);
            //    foreach (MultipartFileData file in provider.FileData)
            //    {
            //        Trace.WriteLine(file.Headers.ContentDisposition.FileName);
            //        Trace.WriteLine("Server file path: " + file.LocalFileName);
            //    }
            //    return Request.CreateResponse(HttpStatusCode.OK);
            //}
            //catch (System.Exception e)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            //}
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReportCases.Add(reportCase);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reportCase.ReportCaseID }, reportCase);
        }

        // DELETE: api/ReportCasesAPI/5
        [ResponseType(typeof(ReportCase))]
        public async Task<IHttpActionResult> DeleteReportCase(int id)
        {
            ReportCase reportCase = await db.ReportCases.FindAsync(id);
            if (reportCase == null)
            {
                return NotFound();
            }

            db.ReportCases.Remove(reportCase);
            await db.SaveChangesAsync();

            return Ok(reportCase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportCaseExists(int id)
        {
            return db.ReportCases.Count(e => e.ReportCaseID == id) > 0;
        }
    }
}