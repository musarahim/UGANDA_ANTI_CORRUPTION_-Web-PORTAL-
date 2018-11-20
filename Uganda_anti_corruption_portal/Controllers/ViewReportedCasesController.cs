using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Uganda_anti_corruption_portal.Models;
using System.Net;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class ViewReportedCaseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ViewReportedCase
        [Authorize]
        public ActionResult Index()
        {
          
           var viewedCases = db.ViewReportedCases.Include(c => c.ReportCase).Include(c=>c.Office);
            return View(viewedCases.ToList());
        }

        
        // GET: Activities/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewReportedCase view = db.ViewReportedCases.Find(id);
            if (view == null)
            {
                return HttpNotFound();
            }
            return View(view);
        }

        // GET: Activities/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ReportCaseID = new SelectList(db.ReportCases, "ReportCaseID", "Title");
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ViewReportedCaseID,ReportCaseID,OfficeID,Comment")] ViewReportedCase viewReportedCase)
        {
            if (ModelState.IsValid)
            {
                    db.ViewReportedCases.Add(viewReportedCase);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }

            ViewBag.ReportCaseID = new SelectList(db.ReportCases, "ReportCaseID", "Title",viewReportedCase.ReportCaseID);
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName",viewReportedCase.OfficeID);
            return View(viewReportedCase);

        }

        // GET: Activities/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewReportedCase view = db.ViewReportedCases.Find(id);
            if (view == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportCaseID = new SelectList(db.ReportCases, "ReportCaseID", "Title",view.ReportCaseID);
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName",view.OfficeID);
            return View(view);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ViewReportedCaseID,ReportCaseID,OfficeID,Comment")] ViewReportedCase viewReportedCase)
        {
            if (ModelState.IsValid)
            {
                    db.Entry(viewReportedCase).State = EntityState.Modified;
                    db.SaveChanges();
                  
                    return RedirectToAction("Index");
               
            }
            ViewBag.ReportCaseID = new SelectList(db.ReportCases, "ReportCaseID", "Title",viewReportedCase.ReportCaseID);
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName", viewReportedCase.OfficeID);
            return View(viewReportedCase);
        }

        // GET: Activities/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewReportedCase viewReportedCase = db.ViewReportedCases.Find(id);
            if (viewReportedCase == null)
            {
                return HttpNotFound();
            }
            return View(viewReportedCase);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewReportedCase activity = db.ViewReportedCases.Find(id);
            db.ViewReportedCases.Remove(activity);
            db.SaveChanges();
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