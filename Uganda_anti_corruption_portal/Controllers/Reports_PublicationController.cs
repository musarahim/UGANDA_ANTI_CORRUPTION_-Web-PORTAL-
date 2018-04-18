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
    public class Reports_PublicationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports_Publication
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Index()
        {
            return View(db.Reports_Publication.ToList());
        }
        public ActionResult Legislation()
        {
            var publications = db.Reports_Publication.Where(c => c.ReportsType ==publicationType.Legislation );
            return View(publications.ToList());
        }
        public ActionResult Reports()
        {
            var publications = db.Reports_Publication.Where(c => c.ReportsType == publicationType.Reports);
            return View(publications.ToList());
        }
        public ActionResult Publications()
        {
            var publications = db.Reports_Publication.Where(c => c.ReportsType == publicationType.publications);
            return View(publications.ToList());
        }

        //Get File upload.pdf
        public FileResult GetFile(int ID)
        {
            var Reports = db.Reports_Publication.Find(ID);
            if (Reports != null)
            {
                return File(Reports.FileContent, Reports.FileName);
            }
            else
            {
                return null;
            }
        }

        // GET: Reports_Publication/Details/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports_Publication reports_Publication = db.Reports_Publication.Find(id);
            if (reports_Publication == null)
            {
                return HttpNotFound();
            }
            return View(reports_Publication);
        }

        // GET: Reports_Publication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports_Publication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reports_PublicationID,Title,ReportsType,PublicationDate,Details")] Reports_Publication reports_Publication,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                String FileExt = Path.GetExtension(file.FileName).ToUpper();
                if (file!=null && FileExt == ".PDF")
                {
                    reports_Publication.FileName = file.ContentType;
                    reports_Publication.FileContent = new byte[file.ContentLength];
                    file.InputStream.Read(reports_Publication.FileContent,0, file.ContentLength);
                    reports_Publication.PublicationDate = System.DateTime.Now.Date;
                    db.Reports_Publication.Add(reports_Publication);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.FileStatus = "Invalid file format.Please insert only pdf files";
                }
               
            }

            return View(reports_Publication);
        }

        // GET: Reports_Publication/Edit/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports_Publication reports_Publication = db.Reports_Publication.Find(id);
            if (reports_Publication == null)
            {
                return HttpNotFound();
            }
            return View(reports_Publication);
        }

        // POST: Reports_Publication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reports_PublicationID,Title,ReportsType,PublicationDate,Details")] Reports_Publication reports_Publication, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
               
                if (file != null)
                {
                    reports_Publication.FileName = file.ContentType;
                    reports_Publication.FileContent = new byte[file.ContentLength];
                    file.InputStream.Read(reports_Publication.FileContent, 0, file.ContentLength);
                     reports_Publication.PublicationDate = System.DateTime.Now.Date;
                    db.Entry(reports_Publication).State = EntityState.Modified;
                    db.SaveChanges();
                 }
                    return RedirectToAction("Index");
              
                
                
            }
            return View(reports_Publication);
        }

        // GET: Reports_Publication/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports_Publication reports_Publication = db.Reports_Publication.Find(id);
            if (reports_Publication == null)
            {
                return HttpNotFound();
            }
            return View(reports_Publication);
        }

        // POST: Reports_Publication/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reports_Publication reports_Publication = db.Reports_Publication.Find(id);
            db.Reports_Publication.Remove(reports_Publication);
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
