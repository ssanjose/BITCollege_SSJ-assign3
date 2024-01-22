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
    /// StudentsController: Controller for Student Model.
    /// </summary>
    public class StudentsController : Controller
    {
        private BITCollege_SSJContext db = new BITCollege_SSJContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.AcademicProgram).Include(s => s.GradePointState);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.AcademicProgramId = new SelectList(db.AcademicPrograms, "AcademicProgramId", "ProgramAcronym");
            ViewBag.GradePointStateId = new SelectList(db.GradePointStates, "GradePointStateId", "Description");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,GradePointStateId,AcademicProgramId,StudentNumber,FirstName,LastName,Address,City,Province,PostalCode,DateCreated,GradePointAverage,OutstandingFees,Notes")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.Students.Where(x => x.StudentId == student.StudentId).SingleOrDefault().ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicProgramId = new SelectList(db.AcademicPrograms, "AcademicProgramId", "ProgramAcronym", student.AcademicProgramId);
            ViewBag.GradePointStateId = new SelectList(db.GradePointStates, "GradePointStateId", "Description", student.GradePointStateId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.AcademicProgramId = new SelectList(db.AcademicPrograms, "AcademicProgramId", "ProgramAcronym", student.AcademicProgramId);
            ViewBag.GradePointStateId = new SelectList(db.GradePointStates, "GradePointStateId", "Description", student.GradePointStateId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,GradePointStateId,AcademicProgramId,StudentNumber,FirstName,LastName,Address,City,Province,PostalCode,DateCreated,GradePointAverage,OutstandingFees,Notes")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.Students.Where(x => x.StudentId == student.StudentId).SingleOrDefault().ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicProgramId = new SelectList(db.AcademicPrograms, "AcademicProgramId", "ProgramAcronym", student.AcademicProgramId);
            ViewBag.GradePointStateId = new SelectList(db.GradePointStates, "GradePointStateId", "Description", student.GradePointStateId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
