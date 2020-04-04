﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance6.Models;

namespace CarInsurance6.Controllers
{
    public class InsureesController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: Insurees
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insurees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insurees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insurees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {

            if (ModelState.IsValid)
            {


                insuree.Quote = 50;
                DateTime present = DateTime.Now;
                if ((present.Year - insuree.DateOfBirth.Year) < 25)
                {
                    insuree.Quote += 25;
                }
                if ((present.Year - insuree.DateOfBirth.Year) < 18)
                {
                    insuree.Quote += 100;
                }
                if ((present.Year - insuree.DateOfBirth.Year) > 100)
                {
                    insuree.Quote += 25;
                }
                if (insuree.CarYear < 2000)
                {
                    insuree.Quote += 25;
                }
                if (insuree.CarMake == "Porsche")
                {
                    insuree.Quote += 25;
                }
                if (insuree.CarMake == "Porsche" && insuree.CarModel == "911 Carrera")
                {
                    insuree.Quote += 25;
                }
                if (insuree.SpeedingTickets > 0 )
                {
                    insuree.Quote += insuree.SpeedingTickets *10;
                }





                if (insuree.DUI == true)
                {
                    insuree.Quote += ((25 / 100) * insuree.Quote);
                }
                if (insuree.CoverageType == true)
                {
                    insuree.Quote += ((50 / 100) * insuree.Quote);
                }
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");



            }
            return View(insuree);
        }

        // POST: Insurees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insurees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insurees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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
