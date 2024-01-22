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
    /// AcademicProgramsController: Controller for AcademicProgram model.
    /// </summary>
    public class AcademicProgramsController : Controller
    {
        private BITCollege_SSJContext db = new BITCollege_SSJContext();

        // GET: AcademicPrograms
        public ActionResult Index()
        {
            return View(db.AcademicPrograms.ToList());
        }

        // GET: AcademicPrograms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicProgram academicProgram = db.AcademicPrograms.Find(id);
            if (academicProgram == null)
            {
                return HttpNotFound();
            }
            return View(academicProgram);
        }

        // GET: AcademicPrograms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AcademicPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AcademicProgramId,ProgramAcronym,Description")] AcademicProgram academicProgram)
        {
            if (ModelState.IsValid)
            {
                db.AcademicPrograms.Add(academicProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicProgram);
        }

        // GET: AcademicPrograms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicProgram academicProgram = db.AcademicPrograms.Find(id);
            if (academicProgram == null)
            {
                return HttpNotFound();
            }
            return View(academicProgram);
        }

        // POST: AcademicPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AcademicProgramId,ProgramAcronym,Description")] AcademicProgram academicProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicProgram);
        }

        // GET: AcademicPrograms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicProgram academicProgram = db.AcademicPrograms.Find(id);
            if (academicProgram == null)
            {
                return HttpNotFound();
            }
            return View(academicProgram);
        }

        // POST: AcademicPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcademicProgram academicProgram = db.AcademicPrograms.Find(id);

            //  Clears the navigability property of this.academicProgram so that it doesn't delete the
            //      related child and course records.
            academicProgram.Student.Clear();
            academicProgram.Course.Clear();
            //  Result: academicProgram is detached from it's corresponding records from
            //          student and course tables.

            db.AcademicPrograms.Remove(academicProgram);
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
