using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Uganda_anti_corruption_portal.Models;
using Uganda_anti_corruption_portal.ViewModel;


namespace Uganda_anti_corruption_portal.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //Get:Activities to anonymous users
        public int PageSize = 6;
        public object CurrentCategory { get; private set; }
        public ViewResult Activity(string category, int page = 1)
        {

            var ViewModel = new DisplayActivityModel
            {
                Contributors = db.Contributors.ToList().Distinct(),
                Activities = db.activities
                .Include(a => a.Contributor)
                 .Include(a => a.ActivityCateory).Where(c => category == null || c.ActivityCateory.Category == category )
                 .OrderBy(c => c.ContributorID).ThenBy(c=>c.ActivityNo).ToList()
                 .Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                db.activities.Count() : db.activities.Where(c => c.ActivityCateory.Category == category).Count()
                },
                CurrentCategory = category
            };
            ViewModel.Contributors = db.Contributors;
            ViewModel.ActivityCategories = db.ActivityCategories; 
            return View(ViewModel);
        }

        // GET: Activities
        [Authorize]
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var ContributorID = db.Contributors.Where(u => u.ApplicationUserId == UserId).First().ContributorID;
            ViewBag.ContributorID = ContributorID;
            var activities = db.activities.Include(a => a.ActivityCateory).Where(a=>a.ContributorID==ContributorID);
            return View(activities.ToList());
        }

        //GET: Image method
        public FileResult getImage(int ID)
        {
            var activity = db.activities.Find(ID);
            if (activity != null)
            {
                return File(activity.ImageData, activity.ImageType);
            }
            else
            {
                return null;
            }
        }

        // GET: Activities/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        [Authorize]
        public ActionResult Create()
        {
          
              var UserId = User.Identity.GetUserId();
            var ContributorID = db.Contributors.Where(u => u.ApplicationUserId == UserId).First().ContributorID;
            ViewBag.ContributorID = ContributorID;
            ViewBag.ActivityCategoryID = new SelectList(db.ActivityCategories, "ActivityCategoryID", "NameOfService");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityID,ActivityCategoryID,ActivityNo,NameOfActivity,Description")] Activity activity, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                String FileExt = Path.GetExtension(Image.FileName).ToUpper();
                if ((Image != null && FileExt == ".JPEG")||(Image!=null && FileExt==".PNG")|| (Image != null && FileExt == ".BPM") || (Image != null && FileExt == ".JPG"))

                {
                    //image data
                    activity.ImageType = Image.ContentType;
                    activity.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(activity.ImageData, 0, Image.ContentLength);
               
               
                var UserId = User.Identity.GetUserId();
                var ContributorID = db.Contributors.Where(u => u.ApplicationUserId == UserId).First().ContributorID;
                activity.ContributorID = ContributorID;
                db.activities.Add(activity);
                db.SaveChanges();
                TempData["notice"] = "Successfully inserted one Step";
                return RedirectToAction("Create");
                }
            else
            {
                ViewBag.FileStatus = "Invalid file format.Please insert only picture files";
            }
            }

            ViewBag.ActivityCategoryID = new SelectList(db.ActivityCategories, "ActivityCategoryID", "NameOfService", activity.ActivityCategoryID);
            return View(activity);

        }

        // GET: Activities/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityCategoryID = new SelectList(db.ActivityCategories, "ActivityCategoryID", "NameOfService", activity.ActivityCategoryID);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityID,ActivityCategoryID,ActivityNo,NameOfActivity,Description")] Activity activity,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                String FileExt = Path.GetExtension(Image.FileName).ToUpper();
                if ((Image != null && FileExt == ".JPEG") || (Image != null && FileExt == ".PNG") || (Image != null && FileExt == ".BPM") || (Image != null && FileExt == ".JPG"))

                    {
                        //image data
                       activity.ImageType = Image.ContentType;
                activity.ImageData = new byte[Image.ContentLength];
                Image.InputStream.Read(activity.ImageData, 0, Image.ContentLength);
                var UserId = User.Identity.GetUserId();
                var ContributorID = db.Contributors.Where(u => u.ApplicationUserId == UserId).First().ContributorID;
                activity.ContributorID = ContributorID;
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Updatenotice"] = "Successfully updated! ";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.FileStatus = "Invalid file format.Please insert only picture files";
                }
            }
            ViewBag.ActivityCategoryID = new SelectList(db.ActivityCategories, "ActivityCategoryID", "NameOfService", activity.ActivityCategoryID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.activities.Find(id);
            db.activities.Remove(activity);
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
