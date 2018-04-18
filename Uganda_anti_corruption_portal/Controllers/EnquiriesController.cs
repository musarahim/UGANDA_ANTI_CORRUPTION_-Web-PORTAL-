using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class EnquiriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enquiries
        [Authorize(Users = "pr@igg.go.ug")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Enquiries.OrderByDescending(c=>c.EnquiryDate).ToListAsync());
        }

        // GET: Enquiries/Details/5
        [Authorize(Users = "pr@igg.go.ug")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enquiry enquiry = await db.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return HttpNotFound();
            }
            return View(enquiry);
        }

        // GET: Enquiries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enquiries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnquiryID,Name,Email,Phone,Subject,Message,EnquiryDate")] Enquiry enquiry)
        {
            if (ModelState.IsValid)
            {
                enquiry.EnquiryDate = System.DateTime.Now;
                db.Enquiries.Add(enquiry);
                await db.SaveChangesAsync();
                TempData["notice"] = "We have received your Message.We shall shortly send you a reply";

                return RedirectToAction("Create");
            }

            return View(enquiry);
        }

        // GET: Enquiries/Edit/5
        [Authorize(Users = "pr@igg.go.ug")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enquiry enquiry = await db.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return HttpNotFound();
            }
            return View(enquiry);
        }

        // POST: Enquiries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnquiryID,Name,Email,Phone,Subject,Message,EnquiryDate")] Enquiry enquiry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enquiry).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(enquiry);
        }

        // GET: Enquiries/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enquiry enquiry = await db.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return HttpNotFound();
            }
            return View(enquiry);
        }

        // POST: Enquiries/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enquiry enquiry = await db.Enquiries.FindAsync(id);
            db.Enquiries.Remove(enquiry);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
