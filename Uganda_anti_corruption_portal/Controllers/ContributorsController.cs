using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class ContributorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contributors
        public ActionResult Index()
        {
            return View(db.Contributors.ToList());
        }

        // GET: Contributors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contributor contributor = db.Contributors.Find(id);
            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }

        // GET: Contributors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contributors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContributorID,Names,Location,references")] Contributor contributor)
        {
            if (ModelState.IsValid)
            {
                db.Contributors.Add(contributor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contributor);
        }

        // GET: Contributors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contributor contributor = db.Contributors.Find(id);
            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }

        // POST: Contributors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContributorID,Names,Location,references")] Contributor contributor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contributor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contributor);
        }

        // GET: Contributors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contributor contributor = db.Contributors.Find(id);
            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }

        // POST: Contributors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contributor contributor = db.Contributors.Find(id);
            db.Contributors.Remove(contributor);
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
