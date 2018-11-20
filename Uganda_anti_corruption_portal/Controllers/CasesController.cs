using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class CasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cases
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Index()
        {
            return View(db.Cases.ToList());
        }
        //ongoingcriminal cases report
        public ActionResult OnGoingCriminalReport()
        {

            var onGoingCases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Criminal).Where(c => c.CaseStatus == CaseStatus.OnGoing).OrderBy(c => c.UpdatedDate).ToList();
            CrystalReports.OnGoingCriminalCases report = new CrystalReports.OnGoingCriminalCases();
            report.Load();
            report.SetDataSource(onGoingCases);
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            if (onGoingCases == null)
            {
                return HttpNotFound();
            }
            return File(stream, "application/pdf");
        }


        //returns a list of all criminal cases

        public ActionResult CrimalCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Criminal);
            return View(cases.ToList());
        }
        public ActionResult CivilCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Civil);
            return View(cases.ToList());
        }
        public ActionResult OnGoingCrimalCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Criminal).Where(c=>c.CaseStatus==CaseStatus.OnGoing);
            return View(cases.ToList());
        }
        public ActionResult OnGoingCivilCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Civil).Where(c => c.CaseStatus == CaseStatus.OnGoing);
            return View(cases.ToList());
        }
        //concluded criminal cases
        public ActionResult ConcludedCrimalCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Criminal).Where(c => c.CaseStatus == CaseStatus.Concluded);
            return View(cases.ToList());
        }
        //applealed criminal cases
        public ActionResult AppealedCrimalCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Criminal).Where(c => c.CaseStatus == CaseStatus.Appealed);
            return View(cases.ToList());
        }
        //appealed civil cases
        public ActionResult AppealedCivilCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Civil).Where(c => c.CaseStatus == CaseStatus.Appealed);
            return View(cases.ToList());
        }
        //concluded civil cases
        public ActionResult ConcludedCivilCases()
        {
            var cases = db.Cases.Where(c => c.CaseCategory == CaseCategory.Civil).Where(c => c.CaseStatus == CaseStatus.Concluded);
            return View(cases.ToList());
        }
        // GET: Cases/Details/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // GET: Cases/Create
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Create()
        {
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName");
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CaseID,CaseRef,CaseStatus,CaseCategory,Description,UpdatedDate,OfficeID")] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Cases.Add(@case);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName");
            return View(@case);
        }

        // GET: Cases/Edit/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName");
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CaseID,CaseRef,CaseStatus,CaseCategory,Description,UpdatedDate,OfficeID")] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfficeID = new SelectList(db.Offices, "OfficeID", "OfficeName");
            return View(@case);
        }

        // GET: Cases/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Case @case = db.Cases.Find(id);
            db.Cases.Remove(@case);
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
