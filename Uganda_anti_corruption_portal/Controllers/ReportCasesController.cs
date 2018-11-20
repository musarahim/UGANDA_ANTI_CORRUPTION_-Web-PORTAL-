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
    public class ReportCasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportCases
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Index()
        {
            return View(db.ReportCases.ToList());
        }
        /// <summary>
        /// Loads crystal reports
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        // the image for the person
        public ActionResult ReportedCasesReport()
        {

            var cases = db.ReportCases.ToList();
            CrystalReports.ReportedCases report = new CrystalReports.ReportedCases();
            report.Load();
            report.SetDataSource(cases);
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            if (cases == null)
            {
                return HttpNotFound();
            }
            return File(stream, "application/pdf");
        }
        public FileContentResult getImage(int ID)
        {
            var Report = db.ReportCases.Find(ID);
            if (Report != null)
            {
                return File(Report.ImageData, Report.ImageType);
            }
            else
            {
                return null;
            }
        }
        // used to get the video files
        public FileContentResult getVideo(int ID)
        {
            var Report = db.ReportCases.Find(ID);
            if (Report != null)
            {
                return File(Report.VideoData, Report.VideoType);
            }
            else
            {
                return null;
            }
        }
        //used to get the audio files
        public FileContentResult getAudio(int ID)
        {
            var Report = db.ReportCases.Find(ID);
            if (Report != null)
            {
                return File(Report.AudioData, Report.AudioType);
            }
            else
            {
                return null;
            }
        }



        // GET: ReportCases/Details/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportCase reportCase = db.ReportCases.Find(id);
            if (reportCase == null)
            {
                return HttpNotFound();
            }
            return View(reportCase);
        }

        // GET: ReportCases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportCaseID,Phone,Title,Description")] ReportCase reportCase, HttpPostedFileBase Image, HttpPostedFileBase Video, HttpPostedFileBase Audio)
        {
            if (ModelState.IsValid)
            {
              //  String FileExt = Path.GetExtension(Image.FileName).ToUpper();
                if (Image != null ){
                    //image data
                    reportCase.ImageType = Image.ContentType;
                    reportCase.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(reportCase.ImageData, 0, Image.ContentLength);
                }
                if (Video != null)
                {
                    //image data
                    reportCase.VideoType = Video.ContentType;
                    reportCase.VideoData = new byte[Video.ContentLength];
                    Video.InputStream.Read(reportCase.VideoData, 0, Video.ContentLength);
                }
                if (Audio != null)
                {
                    //image data
                    reportCase.AudioType = Audio.ContentType;
                    reportCase.AudioData = new byte[Audio.ContentLength];
                    Audio.InputStream.Read(reportCase.AudioData, 0, Audio.ContentLength);
                }
                db.ReportCases.Add(reportCase);
                db.SaveChanges();
                TempData["notice"] = "We have received your complaint.We shall contact you for more information";
                return RedirectToAction("Create");
            }

            return View(reportCase);
        }

        // GET: ReportCases/Edit/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportCase reportCase = db.ReportCases.Find(id);
            if (reportCase == null)
            {
                return HttpNotFound();
            }
            return View(reportCase);
        }

        // POST: ReportCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportCaseID,Phone,Contact,Description")] ReportCase reportCase, HttpPostedFileBase Image, HttpPostedFileBase Video, HttpPostedFileBase Audio)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    //image data
                    reportCase.ImageType = Image.ContentType;
                    reportCase.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(reportCase.ImageData, 0, Image.ContentLength);
                }
                if (Video != null)
                {
                    //image data
                    reportCase.VideoType = Video.ContentType;
                    reportCase.VideoData = new byte[Video.ContentLength];
                    Video.InputStream.Read(reportCase.VideoData, 0, Video.ContentLength);
                }
                if (Audio != null)
                {
                    //image data
                    reportCase.AudioType = Audio.ContentType;
                    reportCase.AudioData = new byte[Audio.ContentLength];
                    Audio.InputStream.Read(reportCase.AudioData, 0, Audio.ContentLength);
                }
                db.Entry(reportCase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportCase);
        }

        // GET: ReportCases/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportCase reportCase = db.ReportCases.Find(id);
            if (reportCase == null)
            {
                return HttpNotFound();
            }
            return View(reportCase);
        }

        // POST: ReportCases/Delete/5
        [Authorize(Users = "pr@igg.go.ug")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportCase reportCase = db.ReportCases.Find(id);
            db.ReportCases.Remove(reportCase);
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
