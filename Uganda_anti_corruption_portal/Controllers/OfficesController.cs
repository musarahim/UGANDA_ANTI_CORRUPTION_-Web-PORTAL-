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
    public class OfficesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Offices

        public ActionResult Index()
        {
            return View(db.Offices.ToList());
        }
        public ActionResult Admin()
        {
            return View(db.Offices.ToList());
        }

        // GET: Offices/Details/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // GET: Offices/Create
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OfficeID,OfficeName,PhysicalLocation,BoxNumber,District,TelNo,Email,Fax")] Office office)
        {
            if (ModelState.IsValid)
            {
                db.Offices.Add(office);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(office);
        }

        // GET: Offices/Edit/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OfficeID,OfficeName,PhysicalLocation,BoxNumber,District,TelNo,Email,Fax")] Office office)
        {
            if (ModelState.IsValid)
            {
                db.Entry(office).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(office);
        }

        // GET: Offices/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Offices/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Office office = db.Offices.Find(id);
            db.Offices.Remove(office);
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
