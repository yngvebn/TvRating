using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class ShowsController : Controller
    {
        private TvShowsContext db = new TvShowsContext();

        // GET: /Shows/
        public ActionResult Index()
        {
            return View(db.TvShowViewModel.ToList());
        }

        // GET: /Shows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShowViewModel tvshowviewmodel = db.TvShowViewModel.Find(id);
            if (tvshowviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(tvshowviewmodel);
        }

        // GET: /Shows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Shows/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TvShowViewModel tvshowviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.TvShowViewModel.Add(tvshowviewmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tvshowviewmodel);
        }

        // GET: /Shows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShowViewModel tvshowviewmodel = db.TvShowViewModel.Find(id);
            if (tvshowviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(tvshowviewmodel);
        }

        [HttpPost]
        public ActionResult Rating(int? id, int rating)
        {
            TvShowViewModel tvshowviewmodel = db.TvShowViewModel.Find(id);
            tvshowviewmodel.AddRating(User.Identity.Name, rating);
            db.Entry(tvshowviewmodel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = id });
        }

        // POST: /Shows/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TvShowViewModel tvshowviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tvshowviewmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tvshowviewmodel);
        }

        // GET: /Shows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShowViewModel tvshowviewmodel = db.TvShowViewModel.Find(id);
            if (tvshowviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(tvshowviewmodel);
        }

        // POST: /Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TvShowViewModel tvshowviewmodel = db.TvShowViewModel.Find(id);
            db.TvShowViewModel.Remove(tvshowviewmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
