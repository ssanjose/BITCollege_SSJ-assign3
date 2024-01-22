using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BITCollege_SSJ.Data;
using BITCollege_SSJ.Models;

/*
 *  Student Name: Symon Kurt San Jose
 *  Program: Business Information Technology
 *  Course: ADEV-3008 (204640) Programming 3
 *  Student ID: 0344572
 *  Date Created: 9/8/2020
 *  Date Updated: 9/15/2020
 */

namespace BITCollege_SSJ.Controllers
{
    /// <summary>
    /// GradePointStatesController: Controller for GradePointState Model.
    /// </summary>
    public class GradePointStatesController : Controller
    {
        private BITCollege_SSJContext db = new BITCollege_SSJContext();

        // GET: GradePointStates
        public ActionResult Index()
        {
            return View(db.GradePointStates.ToList());
        }

        // GET: GradePointStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradePointState gradePointState = db.GradePointStates.Find(id);
            if (gradePointState == null)
            {
                return HttpNotFound();
            }
            return View(gradePointState);
        }

        // GET: GradePointStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GradePointStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradePointStateId,LowerLimit,UpperLimit,TuitionRateFactor")] GradePointState gradePointState)
        {
            if (ModelState.IsValid)
            {
                db.GradePointStates.Add(gradePointState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gradePointState);
        }

        // GET: GradePointStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradePointState gradePointState = db.GradePointStates.Find(id);
            if (gradePointState == null)
            {
                return HttpNotFound();
            }
            return View(gradePointState);
        }

        // POST: GradePointStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradePointStateId,LowerLimit,UpperLimit,TuitionRateFactor")] GradePointState gradePointState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradePointState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gradePointState);
        }

        // GET: GradePointStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradePointState gradePointState = db.GradePointStates.Find(id);
            if (gradePointState == null)
            {
                return HttpNotFound();
            }
            return View(gradePointState);
        }

        // POST: GradePointStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GradePointState gradePointState = db.GradePointStates.Find(id);
            db.GradePointStates.Remove(gradePointState);
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
